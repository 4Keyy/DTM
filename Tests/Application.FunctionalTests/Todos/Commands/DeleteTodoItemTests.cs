using DTM.Application.Todos.Commands.CreateTodoItem;
using DTM.Application.Todos.Commands.DeleteTodoItem;
using DTM.Domain.Entities.Models;

using static Application.FunctionalTests.Testing;

namespace Application.FunctionalTests.Todos.Commands
{
    public class DeleteTodoItemTests : BaseTestFixture
    {
        [Test]
        public async Task ShouldRequireValidTodoListId()
        {
            var command = new DeleteTodoItemCommand(99);
            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteTodoList()
        {
            var todoId = await SendAsync(new CreateTodoItemCommand
            {
                Title = "New List"
            });

            await SendAsync(new DeleteTodoItemCommand(todoId));

            var todo = await FindAsync<TodoItem>(todoId);

            todo.Should().BeNull();
        }
    }
}
