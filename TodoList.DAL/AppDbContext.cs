using Microsoft.EntityFrameworkCore;
using Todolist.DAL.Models;
using TodoList.Domain.Entity;
using Todolist.DAL.Configurations;

namespace Todolist.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Assignment> Tasks { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AssignmentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        
        base.OnModelCreating(modelBuilder);
        base.Database.Migrate();
    }
}

