using DTM.Application.Common.Models;

namespace DTM.Application.Categories.Queries.GetCategories
{
    public class CategoriesVm
    {
        public IReadOnlyCollection<LookupDto> Colours { get; init; } = Array.Empty<LookupDto>();
    }
}
