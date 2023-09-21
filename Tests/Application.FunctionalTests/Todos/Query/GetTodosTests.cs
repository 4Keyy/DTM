using DTM.Application.Todos.Queries.GetTodos;
using DTM.Domain.Entities.Models;
using DTM.Domain.Enums;

using static Application.FunctionalTests.Testing;

namespace Application.FunctionalTests.Todos.Query
{
    public class GetTodosTests : BaseTestFixture
    {
        [Test]
        public async Task ShouldReturnPriorityLevels()
        {
            await RunAsDefaultUserAsync();

            var query = new GetTodosQuery();

            var result = await SendAsync(query);

            result.PriorityLevels.Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldReturnAllItems()
        {
            await RunAsDefaultUserAsync();

            await AddAsync(new TodoItem
            {
                Title = "Shopping",
                Description = "",
                IsDone = false,
                PriorityLevel = PriorityLevel.Low
            });

            var query = new GetTodosQuery();

            var result = await SendAsync(query);

            result.Categories.Should().HaveCount(1);
            result.PriorityLevels.First().Title!.ToString().Length.Should().Equals(3);
        }

        [Test]
        public async Task ShouldDenyAnonymousUser()
        {
            var query = new GetTodosQuery();

            var action = () => SendAsync(query);

            await action.Should().ThrowAsync<UnauthorizedAccessException>();
        }
    }
}
