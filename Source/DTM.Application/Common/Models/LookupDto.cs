using DTM.Domain.Entities.Models;

namespace DTM.Application.Common.Models
{
    public class LookupDto
    {
        public int Id { get; init; }

        public string? Title { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Category, LookupDto>();
                CreateMap<Note, LookupDto>();
                CreateMap<TodoItem, LookupDto>();
            }
        }
    }
}
