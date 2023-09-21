using DTM.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DTM.Infrastructure.Data.Configurations.CategoryConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(category => category.Title).HasMaxLength(200).IsRequired();
            builder.Property(category => category.Description).HasMaxLength(2000);
            builder.Property(category => category.Colour).IsRequired();
        }
    }
}
