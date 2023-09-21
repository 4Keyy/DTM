using DTM.Application.Common.Interfaces;
using DTM.Domain.Entities.Models;
using DTM.Domain.Events;

namespace DTM.Application.Todos.Commands.DeleteTodoItem
{
    public record DeleteTodoItemCommand(int Id) : IRequest
    {

    }

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Todos.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            _context.Todos.Remove(entity);

            entity.AddDomainEvent(new DeletedEvent<TodoItem>(entity));

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
