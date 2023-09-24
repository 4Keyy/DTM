using DTM.Application.Common.Interfaces;
using DTM.Domain.Entities.Models;
using DTM.Domain.Events;
using DTM.Domain.ValueObjects;

namespace DTM.Application.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand : IRequest<int>
    {
        public string Title { get; init; }

        public string? Description { get; init; }

        public Colour Colour { get; init; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new Category
            {
                Title = request.Title,
                Description = request.Description,
                Colour = request.Colour
            };

            entity.AddDomainEvent(new CreateEvent<Category>(entity));

            _context.Categories.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
