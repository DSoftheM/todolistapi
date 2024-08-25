using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todolist.DAL.Models;

namespace TodoList.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<CourseEntity>
{
    public void Configure(EntityTypeBuilder<CourseEntity> builder)
    {
        builder.HasKey(x => x.Id);

        // Дубликат связи
        builder.HasOne(c => c.Author)
            .WithOne(a => a.Course)
            .HasForeignKey<CourseEntity>(c => c.AuthorId);

        builder.HasMany(c => c.Lessons)
            .WithOne(l => l.Course)
            .HasForeignKey(l => l.CourseId);

        builder
            .HasMany(c => c.Students)
            .WithMany(s => s.Courses);
    }
}