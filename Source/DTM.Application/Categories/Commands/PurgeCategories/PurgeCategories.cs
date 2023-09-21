using DTM.Application.Common.Attributes.Security;
using DTM.Application.Common.Interfaces;
using DTM.Domain.Constants;

namespace DTM.Application.Categories.Commands.PurgeCategories
{
    [Authorize(Roles = Roles.Administrator)]
    [Authorize(Policy = Policies.CanPurge)]
    public record PurgeCategoriesCommand : IRequest
    {

    }

    public class PurgeCategoriesCommandHandler : IRequestHandler<PurgeCategoriesCommand>
    {
        private readonly IApplicationDbContext _context;

        public PurgeCategoriesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(PurgeCategoriesCommand request, CancellationToken cancellationToken)
        {
            _context.Categories.RemoveRange(_context.Categories);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
