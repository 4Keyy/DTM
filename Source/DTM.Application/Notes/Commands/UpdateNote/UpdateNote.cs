using DTM.Application.Common.Interfaces;

namespace DTM.Application.Notes.Commands.UpdateNote
{
    public record UpdateNoteCommand : IRequest
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public string Description { get; init; }
    }

    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateNoteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notes.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Title = request.Title;
            entity.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
