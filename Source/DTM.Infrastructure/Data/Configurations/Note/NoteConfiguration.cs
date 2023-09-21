using DTM.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DTM.Infrastructure.Data.Configurations.NoteConfigurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.Property(note => note.Title).HasMaxLength(200).IsRequired();
            builder.Property(note => note.IsEmergency).HasDefaultValue(false);
        }
    }
}
