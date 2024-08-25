using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todolist.DAL.Models;

namespace TodoList.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
{
    public void Configure(EntityTypeBuilder<AuthorEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        // Дубликат связи
        builder
            .HasOne(a => a.Course).WithOne(c => c.Author)
            .HasForeignKey<AuthorEntity>(a => a.CourseId);
    }
}