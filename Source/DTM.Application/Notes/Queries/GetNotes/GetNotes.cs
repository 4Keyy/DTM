using DTM.Application.Common.Attributes.Security;
using DTM.Application.Common.Interfaces;
using DTM.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DTM.Application.Notes.Queries.GetNotes
{
    [Authorize]
    public record GetNotesQuery : IRequest<NotesVm>
    {

    }

    public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, NotesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetNotesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NotesVm> Handle(GetNotesQuery request, CancellationToken cancellationToken)
        {
            return new NotesVm
            {
                Categories = (IReadOnlyCollection<LookupDto>)await _context.Notes.AsNoTracking().
                    ProjectTo<NoteDto>(_mapper.ConfigurationProvider).
                    OrderBy(category => category.Id).ToListAsync(cancellationToken)
            };
        }
    }
}
