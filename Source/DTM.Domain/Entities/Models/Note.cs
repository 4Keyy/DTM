using DTM.Domain.Common;
using DTM.Domain.Events;

namespace DTM.Domain.Entities.Models
{
    public class Note : BaseAuditableEntity
    {
        public string Title { get; set; }

        public string? Description { get; set; } = "";

        public Category? Category { get; set; }

        private bool _isEmergency;
        public bool IsEmergency
        {
            get
            {
                return _isEmergency;
            }

            set
            {
                if (value && !_isEmergency)
                {
                    AddDomainEvent(new StatusChangeEvent<Note>(this));
                }

                _isEmergency = value;
            }
        }
    }
}
