using DTM.Domain.Entities.Models;
using DTM.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DTM.Infrastructure.Data.Configurations.CategoryConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(category => category.Title).HasMaxLength(64).IsRequired();
            builder.Property(category => category.Description).HasDefaultValue("").HasMaxLength(2048).IsRequired();
            builder.Property(category => category.Colour).HasDefaultValue(Colour.White).IsRequired();
        }
    }
}
