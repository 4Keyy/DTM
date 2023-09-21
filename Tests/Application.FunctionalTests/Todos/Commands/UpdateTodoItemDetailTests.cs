using DTM.Application.Todos.Commands.CreateTodoItem;
using DTM.Application.Todos.Commands.UpdateTodoItem;
using DTM.Application.Todos.Commands.UpdateTodoItemDetail;
using DTM.Domain.Entities.Models;
using DTM.Domain.Enums;

using static Application.FunctionalTests.Testing;

namespace Application.FunctionalTests.Todos.Commands
{
    public class UpdateTodoItemDetailTests : BaseTestFixture
    {
        [Test]
        public async Task ShouldRequireValidTodoItemId()
        {
            var command = new UpdateTodoItemCommand { Id = 99, Title = "New Title" };
            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateTodoItem()
        {
            var userId = await RunAsDefaultUserAsync();

            var itemId = await SendAsync(new CreateTodoItemCommand
            {
                Title = "New Item"
            });

            var command = new UpdateTodoItemDetailCommand
            {
                Id = itemId,
                Description = "This is the note.",
                PriorityLevel = PriorityLevel.High
            };

            await SendAsync(command);

            var item = await FindAsync<TodoItem>(itemId);

            item.Should().NotBeNull();
            item.Description.Should().Be(command.Description);
            item.PriorityLevel.Should().Be(command.PriorityLevel);
            item.LastModifiedBy.Should().NotBeNull();
            item.LastModifiedBy.Should().Be(userId);
            item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        }
    }
}
