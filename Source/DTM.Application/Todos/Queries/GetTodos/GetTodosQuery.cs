using DTM.Application.Common.Attributes.Security;
using DTM.Application.Common.Interfaces;
using DTM.Application.Common.Models;
using DTM.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DTM.Application.Todos.Queries.GetTodos
{
    [Authorize]
    public record GetTodosQuery : IRequest<TodosVm>
    {

    }

    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            return new TodosVm
            {
                PriorityLevels = Enum.GetValues(typeof(PriorityLevel)).Cast<PriorityLevel>().
                    Select(priorityLevel => new LookupDto { Id = (int)priorityLevel, Title = priorityLevel.ToString() })
                    .ToList(),

                Categories = (IReadOnlyCollection<LookupDto>)await _context.Todos.AsNoTracking()
                    .ProjectTo<TodoItemDto>(_mapper.ConfigurationProvider).
                    OrderBy(category => category.Id).ToListAsync(cancellationToken)
            };   
        }
    }
}
