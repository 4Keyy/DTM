using DTM.Application.Common.Interfaces;
using DTM.Domain.ValueObjects;

namespace DTM.Application.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand : IRequest
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public Colour Colour { get; init; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Title = request.Title;
            entity.Colour = request.Colour;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
