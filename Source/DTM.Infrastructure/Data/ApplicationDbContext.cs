using System.Reflection;
using DTM.Application.Common.Interfaces;
using DTM.Domain.Entities.Models;
using DTM.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DTM.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) 
        {
        
        }

        public DbSet<TodoItem> Todos => Set<TodoItem>();

        public DbSet<Note> Notes => Set<Note>();

        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
