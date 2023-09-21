using DTM.Application.Common.Interfaces;

namespace DTM.Application.Todos.Commands.UpdateTodoItem
{
    public record UpdateTodoItemCommand : IRequest
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public bool IsDone { get; init; }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Todos.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Title = request.Title;
            entity.IsDone = request.IsDone;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
