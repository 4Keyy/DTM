using DTM.Application.Common.Attributes.Security;
using DTM.Application.Common.Exceptions;
using DTM.Application.Todos.Commands.CreateTodoItem;
using DTM.Application.Todos.Commands.PurgeTodoItems;
using DTM.Domain.Entities.Models;

using static Application.FunctionalTests.Testing;

namespace Application.FunctionalTests.Todos.Commands
{
    public class PurgeTodoItemTests : BaseTestFixture
    {
        [Test]
        public async Task ShouldDenyAnonymousUser()
        {
            var command = new PurgeTodoItemCommand();

            command.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();

            var action = () => SendAsync(command);

            await action.Should().ThrowAsync<UnauthorizedAccessException>();
        }

        [Test]
        public async Task ShouldDenyNonAdministrator()
        {
            await RunAsDefaultUserAsync();

            var command = new PurgeTodoItemCommand();

            var action = () => SendAsync(command);

            await action.Should().ThrowAsync<ForbiddenAccessException>();
        }

        [Test]
        public async Task ShouldAllowAdministrator()
        {
            await RunAsAdministratorAsync();

            var command = new PurgeTodoItemCommand();

            var action = () => SendAsync(command);

            await action.Should().NotThrowAsync<ForbiddenAccessException>();
        }

        [Test]
        public async Task ShouldDeleteAllLists()
        {
            await RunAsAdministratorAsync();

            await SendAsync(new CreateTodoItemCommand
            {
                Title = "New Todo #1"
            });

            await SendAsync(new CreateTodoItemCommand
            {
                Title = "New Todo #2"
            });

            await SendAsync(new CreateTodoItemCommand
            {
                Title = "New Todo #3"
            });

            await SendAsync(new PurgeTodoItemCommand());

            var count = await CountAsync<TodoItem>();

            count.Should().Be(0);
        }
    }
}
