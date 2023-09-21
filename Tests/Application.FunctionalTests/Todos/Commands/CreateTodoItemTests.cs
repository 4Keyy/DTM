using DTM.Application.Common.Exceptions;
using DTM.Application.Todos.Commands.CreateTodoItem;
using DTM.Domain.Entities.Models;

using static Application.FunctionalTests.Testing;

namespace Application.FunctionalTests.Todos.Commands
{
    public class CreateTodoItemTests : BaseTestFixture
    {
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            var command = new CreateTodoItemCommand();
            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueTitle()
        {
            await SendAsync(new CreateTodoItemCommand
            {
                Title = "Shopping"
            });

            var command = new CreateTodoItemCommand
            {
                Title = "Shopping"
            };

            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateTodoList()
        {
            var userId = await RunAsDefaultUserAsync();

            var command = new CreateTodoItemCommand
            {
                Title = "Tasks"
            };

            var id = await SendAsync(command);

            var todo = await FindAsync<TodoItem>(id);

            todo.Should().NotBeNull();
            todo!.Title.Should().Be(command.Title);
            todo.CreatedBy.Should().Be(userId);
            todo.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        }
    }
}
