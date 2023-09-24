using DTM.Domain.Common;
using DTM.Domain.ValueObjects;

namespace DTM.Domain.Entities.Models
{
    public class Category : BaseAuditableEntity
    {
        public string Title { get; set; }

        public string? Description { get; set; } = "";

        public Colour Colour { get; set; }
    }
}
