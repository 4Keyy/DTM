using DTM.Application.Common.Models;

namespace DTM.Application.Todos.Queries.GetTodos
{
    public class TodosVm
    {
        public IReadOnlyCollection<LookupDto> Categories { get; init; } = Array.Empty<LookupDto>();

        public IReadOnlyCollection<LookupDto> PriorityLevels { get; init; } = Array.Empty<LookupDto>();
    }
}
