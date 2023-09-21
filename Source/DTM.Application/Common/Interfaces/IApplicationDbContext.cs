using DTM.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DTM.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<TodoItem> Todos { get; }

        public DbSet<Note> Notes { get; }

        public DbSet<Category> Categories { get; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
