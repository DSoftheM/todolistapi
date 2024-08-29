using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entity;

namespace Todolist.DAL.Configurations;

public class AssignmentEntityConfiguration : IEntityTypeConfiguration<AssignmentEntity>
{
    public void Configure(EntityTypeBuilder<AssignmentEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(task => task.Employee).WithOne(e => e.Task).HasForeignKey<AssignmentEntity>(t => t.EmployeeId);
    }
}