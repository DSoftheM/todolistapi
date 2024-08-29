using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entity;

namespace Todolist.DAL.Configurations;

public class AssignmentEntityConfiguration : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(task => task.Employee).WithOne(e => e.Task).HasForeignKey<Assignment>(t => t.EmployeeId);
    }
}