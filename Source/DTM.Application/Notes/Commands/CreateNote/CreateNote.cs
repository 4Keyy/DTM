using DTM.Application.Common.Interfaces;
using DTM.Domain.Entities.Models;
using DTM.Domain.Events;

namespace DTM.Application.Notes.Commands.CreateNote
{
    public record CreateNoteCommand : IRequest<int>
    {
        public string Title { get; init; }

        public string? Description { get; init; }

        public Category? Category { get; init; }
    }

    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateNoteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = new Note
            {
                Title = request.Title,
                Description = request.Description,
                Category = request.Category
            };

            entity.AddDomainEvent(new CreateEvent<Note>(entity));

            _context.Notes.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
