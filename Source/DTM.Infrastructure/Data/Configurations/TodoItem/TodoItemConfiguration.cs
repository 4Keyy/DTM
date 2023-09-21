using DTM.Domain.Entities.Models;
using DTM.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DTM.Infrastructure.Data.Configurations.TodoItemConfiguration
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.Property(todoItem => todoItem.Title).HasMaxLength(200).IsRequired();
            builder.Property(todoItem => todoItem.Description).HasMaxLength(2000);
            builder.Property(todoItem => todoItem.PriorityLevel).HasDefaultValue(PriorityLevel.None);
            builder.Property(todoItem => todoItem.IsDone).HasDefaultValue(false);
            builder.Property(todoItem => todoItem.IsEmergency).HasDefaultValue(false);
            builder.Property(todoItem => todoItem.Reminder).HasDefaultValue(null);
        }
    }
}
