using DTM.Application.Common.Models;

namespace DTM.Application.Notes.Queries.GetNotes
{
    public class NotesVm
    {
        public IReadOnlyCollection<LookupDto> Categories { get; init; } = Array.Empty<LookupDto>();
    }
}
