using DTM.Application.Common.Attributes.Security;
using DTM.Application.Common.Interfaces;
using DTM.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DTM.Application.Categories.Queries.GetCategories
{
    [Authorize]
    public record GetCategoriesQuery : IRequest<CategoriesVm>
    {

    }

    public class GetCaegoriesQueryHandler : IRequestHandler<GetCategoriesQuery, CategoriesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCaegoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoriesVm> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return new CategoriesVm
            {
                Colours = (IReadOnlyCollection<LookupDto>)await _context.Categories.AsNoTracking()
                    .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).
                    OrderBy(colour => colour.Colour).ToListAsync(cancellationToken)
            };
        }
    }
}
