using DTM.Domain.Entities.Models;

namespace DTM.Application.Notes.Queries.GetNotes
{
    public class NoteDto
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public string? Description { get; init; }

        public string? Category { get; init; }

        public bool IsEmergency {  get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Note, NoteDto>().ForMember(note => note.Category,
                    opt => opt.MapFrom(setting => setting.Category!.Colour.Code));
            }
        }
    }
}
