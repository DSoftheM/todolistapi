using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todolist.DAL.Models;

namespace TodoList.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<LessonEntity>
{
    public void Configure(EntityTypeBuilder<LessonEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(l => l.Course).WithMany(c => c.Lessons).HasForeignKey(l => l.CourseId);
    }
}