using Microsoft.EntityFrameworkCore;
using Todolist.DAL.Models;
using TodoList.Domain.Entity;
using TodoList.Configurations;
using Todolist.DAL.Configurations;

namespace Todolist.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        
        base.OnModelCreating(modelBuilder);
        base.Database.Migrate();
    }
}

