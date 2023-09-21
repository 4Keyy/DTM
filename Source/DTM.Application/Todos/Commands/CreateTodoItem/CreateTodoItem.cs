﻿using DTM.Application.Common.Interfaces;
using DTM.Domain.Entities.Models;
using DTM.Domain.Events;

namespace DTM.Application.Todos.Commands.CreateTodoItem
{
    public record CreateTodoItemCommand : IRequest<int>
    {
        public string Title { get; init; }
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
                IsDone = false
            };

            entity.AddDomainEvent(new CreateEvent<TodoItem>(entity));

            _context.Todos.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
