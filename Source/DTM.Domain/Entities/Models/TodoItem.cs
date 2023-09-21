using DTM.Domain.Common;
using DTM.Domain.Enums;
using DTM.Domain.Events;

namespace DTM.Domain.Entities.Models
{
    public class TodoItem : BaseAuditableEntity
    {
        public required string Title { get; set; }

        public string? Description { get; set; } = "";

        public PriorityLevel PriorityLevel { get; set; }

        public Category? Category { get; set; }

        public DateTime? Reminder { get; set; }

        private bool _isEmergency;
        public bool IsEmergency
        {
            get => _isEmergency;
            set
            {
                if (value && !_isEmergency)
                {
                    AddDomainEvent(new StatusChangeEvent<TodoItem>(this));
                }

                _isEmergency = value;
            }
        }

        private bool _isDone;
        public bool IsDone
        {
            get => _isDone;
            set
            {
                if (value && !_isDone)
                {
                    AddDomainEvent(new CompletedEvent<TodoItem>(this));
                }

                _isDone = value;
            }
        }
    }
}
