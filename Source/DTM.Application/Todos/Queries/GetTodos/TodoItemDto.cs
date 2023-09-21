using DTM.Domain.Entities.Models;

namespace DTM.Application.Todos.Queries.GetTodos
{
    public class TodoItemDto
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public string? Description { get; init; }

        public int PriorityLevel { get; init; }

        public string? Category { get; init; }

        public string? Reminder { get; init; }

        public bool IsEmergency { get; init; }

        public bool IsDone { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<TodoItem, TodoItemDto>();
            }
        }
    }
}
