using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todolist.DAL.Models;

namespace TodoList.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<StudentEntity>
{
    public void Configure(EntityTypeBuilder<StudentEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(s => s.Courses).WithMany(c => c.Students);
    }
}