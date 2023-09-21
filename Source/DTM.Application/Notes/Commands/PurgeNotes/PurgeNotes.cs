using DTM.Application.Common.Attributes.Security;
using DTM.Application.Common.Interfaces;
using DTM.Domain.Constants;

namespace DTM.Application.Notes.Commands.PurgeNotes
{
    [Authorize(Roles = Roles.Administrator)]
    [Authorize(Policy = Policies.CanPurge)]
    public record PurgeNotesCommand : IRequest
    {

    }

    public class PurgeNotesCommandHandler : IRequestHandler<PurgeNotesCommand>
    {
        private readonly IApplicationDbContext _context;

        public PurgeNotesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(PurgeNotesCommand request, CancellationToken cancellationToken)
        {
            _context.Notes.RemoveRange(_context.Notes);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
