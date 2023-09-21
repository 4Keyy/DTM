using DTM.Application.Notes.Commands.CreateNote;
using DTM.Application.Notes.Commands.DeleteNote;
using DTM.Application.Notes.Commands.UpdateNote;
using DTM.Application.Notes.Queries.GetNotes;
using DTM.WebServer.Infrastructure;

namespace DTM.WebServer.Endpoints
{
    public class Notes : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .RequireAuthorization()
                .MapGet(GetNotes)
                .MapPost(CreateNote)
                .MapPut(UpdateNote, "{id}")
                .MapDelete(DeleteNote, "{id}");
        }

        public async Task<NotesVm> GetNotes(ISender sender)
        {
            return await sender.Send(new GetNotesQuery());
        }

        public async Task<int> CreateNote(ISender sender, CreateNoteCommand command)
        {
            return await sender.Send(command);
        }

        public async Task<IResult> UpdateNote(ISender sender, int id, UpdateNoteCommand command)
        {
            if (id != command.Id)
            {
                return Results.BadRequest();
            }
            await sender.Send(command);
            return Results.NoContent();
        }

        public async Task<IResult> DeleteNote(ISender sender, int id)
        {
            await sender.Send(new DeleteNoteCommand(id));
            return Results.NoContent();
        }
    }
}
