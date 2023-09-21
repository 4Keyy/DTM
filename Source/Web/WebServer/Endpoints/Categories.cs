using DTM.Application.Categories.Commands.CreateCategory;
using DTM.Application.Categories.Commands.DeleteCategory;
using DTM.Application.Categories.Commands.UpdateCategory;
using DTM.Application.Categories.Queries.GetCategories;
using DTM.WebServer.Infrastructure;

namespace DTM.WebServer.Endpoints
{
    public class Categories : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoriesVm> GetCategories(ISender sender)
        {
            return await sender.Send(new GetCategoriesQuery());
        }

        public async Task<int> CreateCategory(ISender sender, CreateCategoryCommand command)
        {
            return await sender.Send(command);
        }

        public async Task<IResult> UpdateCategory(ISender sender, int id, UpdateCategoryCommand command)
        {
            if (id != command.Id)
            {
                return Results.BadRequest();
            }
            await sender.Send(command);
            return Results.NoContent();
        }

        public async Task<IResult> DeleteCategory(ISender sender, int id)
        {
            await sender.Send(new DeleteCategoryCommand(id));
            return Results.NoContent();
        }
    }
}
