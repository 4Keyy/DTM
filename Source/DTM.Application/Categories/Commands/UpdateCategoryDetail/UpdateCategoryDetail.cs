using DTM.Application.Common.Interfaces;

namespace DTM.Application.Categories.Commands.UpdateCategoryDetail
{
    public record UpdateCategoryDetailCommand : IRequest
    {
        public int Id { get; init; }

        public string? Description { get; init; }
    }

    public class UpdateCategoryDetailCommandHanlder : IRequestHandler<UpdateCategoryDetailCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryDetailCommandHanlder(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCategoryDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            entity.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
