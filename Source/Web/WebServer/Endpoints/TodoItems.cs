using DTM.Application.Todos.Commands.CreateTodoItem;
using DTM.Application.Todos.Commands.DeleteTodoItem;
using DTM.Application.Todos.Commands.UpdateTodoItem;
using DTM.Application.Todos.Queries.GetTodos;
using DTM.WebServer.Infrastructure;

namespace DTM.WebServer.Endpoints
{
    public class TodoItems : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .RequireAuthorization()
                .MapGet(GetTodoItems)
                .MapPost(CreateTodoItem)
                .MapPut(UpdateTodoItem, "{id}")
                .MapDelete(DeleteTodoItem, "{id}");
        }

        public async Task<TodosVm> GetTodoItems(ISender sender)
        {
            return await sender.Send(new GetTodosQuery());
        }

        public async Task<int> CreateTodoItem(ISender sender, CreateTodoItemCommand command)
        {
            return await sender.Send(command);
        }

        public async Task<IResult> UpdateTodoItem(ISender sender, int id, UpdateTodoItemCommand command)
        {
            if (id != command.Id)
            {
                return Results.BadRequest();
            }
            await sender.Send(command);
            return Results.NoContent();
        }

        public async Task<IResult> DeleteTodoItem(ISender sender, int id)
        {
            await sender.Send(new DeleteTodoItemCommand(id));
            return Results.NoContent();
        }
    }
}
