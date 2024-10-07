using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entity;
using TodoList.Domain.Enum;

namespace Todolist.DAL.Configurations;

public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(task => task.Employees).WithMany(e => e.Tasks);
        builder.Property(x => x.Priority).HasDefaultValue(AssignmentPriority.Low);
    }
}