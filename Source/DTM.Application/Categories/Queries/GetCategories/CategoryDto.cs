using DTM.Domain.Entities.Models;

namespace DTM.Application.Categories.Queries.GetCategories
{
    public class CategoryDto
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public string? Description { get; init; }

        public string Colour { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Category, CategoryDto>().ForMember(category => category.Colour,
                    opt => opt.MapFrom(setting => setting.Colour.Code));
            }
        }
    }
}
