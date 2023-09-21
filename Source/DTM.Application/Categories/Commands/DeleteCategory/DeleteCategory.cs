using DTM.Application.Common.Interfaces;
using DTM.Domain.Entities.Models;
using DTM.Domain.Events;

namespace DTM.Application.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(int Id) : IRequest
    {

    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(request.Id, entity);

            _context.Categories.Remove(entity);

            entity.AddDomainEvent(new DeletedEvent<Category>(entity));

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
