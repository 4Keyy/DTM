using DTM.Application.Categories.Queries.GetCategories;
using DTM.Domain.Entities.Models;
using DTM.Domain.ValueObjects;

using static Application.FunctionalTests.Testing;

namespace Application.FunctionalTests.Categories.Query
{
    public class GetCategoriesTests : BaseTestFixture
    {
        [Test]
        public async Task ShouldReturnColours()
        {
            await RunAsDefaultUserAsync();

            var query = new GetCategoriesQuery();

            var result = await SendAsync(query);

            result.Colours.Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldReturnAllItems()
        {
            await RunAsDefaultUserAsync();

            await AddAsync(new Category
            {
                Title = "Work",
                Description = "",
                Colour = Colour.Blue
            });

            var query = new GetCategoriesQuery();

            var result = await SendAsync(query);

            result.Colours.Should().NotBeNull();
        }

        [Test]
        public async Task ShouldDenyAnonymousUser()
        {
            var query = new GetCategoriesQuery();

            var action = () => SendAsync(query);

            await action.Should().ThrowAsync<UnauthorizedAccessException>();
        }
    }
}
