using Microsoft.EntityFrameworkCore;
using Todolist.DAL.Models;
using TodoList.Domain.Entity;
using TodoList.Configurations;

namespace Todolist.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<TaskEntity> Tasks { get; set; }
    
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<LessonEntity> Lessons { get; set; }
    public DbSet<AuthorEntity> Authors { get; set; }
    public DbSet<StudentEntity> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}

