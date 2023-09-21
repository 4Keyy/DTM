using DTM.Application.Common.Attributes.Security;
using DTM.Application.Common.Interfaces;
using DTM.Domain.Constants;

namespace DTM.Application.Todos.Commands.PurgeTodoItems
{
    [Authorize(Roles = Roles.Administrator)]
    [Authorize(Policy = Policies.CanPurge)]
    public record PurgeTodoItemCommand : IRequest
    {

    }

    public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeTodoItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public PurgeTodoListsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(PurgeTodoItemCommand request, CancellationToken cancellationToken)
        {
            _context.Todos.RemoveRange(_context.Todos);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
