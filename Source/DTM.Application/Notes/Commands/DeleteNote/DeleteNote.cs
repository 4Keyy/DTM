using DTM.Application.Common.Interfaces;
using DTM.Domain.Entities.Models;
using DTM.Domain.Events;

namespace DTM.Application.Notes.Commands.DeleteNote
{
    public record DeleteNoteCommand(int Id) : IRequest
    {

    }

    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteNoteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notes.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            _context.Notes.Remove(entity);

            entity.AddDomainEvent(new DeletedEvent<Note>(entity));

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
