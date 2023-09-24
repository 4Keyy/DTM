using DTM.Application.Common.Interfaces;
using DTM.Domain.Entities.Models;

namespace DTM.Application.Notes.Commands.UpdateNoteDetail
{
    public record UpdateNoteDetailCommand : IRequest
    {
        public int Id { get; init; }

        public Category? Category { get; init; }

        public bool IsEmergency { get; init; }
    }

    public class UpdateNoteDetailCommandHandler : IRequestHandler<UpdateNoteDetailCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateNoteDetailCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateNoteDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notes.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Category = request.Category;
            entity.IsEmergency = request.IsEmergency;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
