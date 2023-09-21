using DTM.Application.Common.Interfaces;
using DTM.Domain.Entities.Models;
using DTM.Domain.Enums;

namespace DTM.Application.Todos.Commands.UpdateTodoItemDetail
{
    public record UpdateTodoItemDetailCommand : IRequest
    {
        public int Id { get; init; }

        public Category Category { get; init; }

        public DateTime Reminder { get; init; }

        public PriorityLevel PriorityLevel { get; init; }

        public string Description { get; init; }
    }

    public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Todos.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Category = request.Category;
            entity.Reminder = request.Reminder;
            entity.PriorityLevel = request.PriorityLevel;
            entity.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
