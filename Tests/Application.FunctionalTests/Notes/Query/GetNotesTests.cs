using DTM.Application.Notes.Queries.GetNotes;
using DTM.Domain.Entities.Models;
using DTM.Domain.ValueObjects;

using static Application.FunctionalTests.Testing;

namespace Application.FunctionalTests.Notes.Query
{
    public class GetNotesTests : BaseTestFixture
    {
        [Test]
        public async Task ShouldReturnCategories()
        {
            await RunAsDefaultUserAsync();

            var query = new GetNotesQuery();

            var result = await SendAsync(query);

            result.Categories.Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldReturnAllItems()
        {
            await RunAsDefaultUserAsync();

            await AddAsync(new Note
            {
                Title = "Shopping",
                Description = "",
                Category = new Category
                {
                    Title = "Life",
                    Colour = Colour.Blue
                }
            });

            var query = new GetNotesQuery();

            var result = await SendAsync(query);

            result.Categories.Should().HaveCount(1);
            result.Categories.First().Title.Should().Equals(3);
        }

        [Test]
        public async Task ShouldDenyAnonymousUser()
        {
            var query = new GetNotesQuery();

            var action = () => SendAsync(query);

            await action.Should().ThrowAsync<UnauthorizedAccessException>();
        }
    }
}
