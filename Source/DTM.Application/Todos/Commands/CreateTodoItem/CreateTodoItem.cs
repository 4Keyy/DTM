using DTM.Application.Common.Interfaces;
using DTM.Domain.Entities.Models;
using DTM.Domain.Enums;
using DTM.Domain.Events;

namespace DTM.Application.Todos.Commands.CreateTodoItem
{
    public record CreateTodoItemCommand : IRequest<int>
    {
        public string Title { get; init; }

        public string Description { get; init; }

        public PriorityLevel PriorityLevel { get; init; }

        public Category? Category { get; init; }

        public DateTime? Reminder { get; init; }

        public bool IsEmergency { get; init; }

        public bool IsDone { get; init; }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoItem
            {
                Title = request.Title,
                Description = request.Description,
                PriorityLevel = request.PriorityLevel,
                Category = request.Category,
                Reminder = request.Reminder,
                IsEmergency = request.IsEmergency,
                IsDone = false
            };

            entity.AddDomainEvent(new CreateEvent<TodoItem>(entity));

            _context.Todos.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
