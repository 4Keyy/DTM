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
            builder.Property(todoItem => todoItem.Title).HasMaxLength(256).IsRequired();
            builder.Property(todoItem => todoItem.Description).HasDefaultValue("").HasMaxLength(2048).IsRequired();
            builder.Property(todoItem => todoItem.PriorityLevel).HasDefaultValue(PriorityLevel.None).IsRequired();
            builder.Property(todoItem => todoItem.Reminder).HasDefaultValue(null);
            builder.Property(todoItem => todoItem.IsEmergency).HasDefaultValue(false).IsRequired();
            builder.Property(todoItem => todoItem.IsDone).HasDefaultValue(false).IsRequired();
        }
    }
}
