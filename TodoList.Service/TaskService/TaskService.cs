using Microsoft.EntityFrameworkCore;
using Todolist.DAL;
using TodoList.Domain.Entity;

namespace TodoList.Service.TaskService;

public class TaskService(AppDbContext dbContext) : ITaskService
{
    public Task<List<TaskEntity>> GetAll()
    {
        return dbContext.Tasks.AsNoTracking().ToListAsync();
    }

    public async Task Create(TaskEntity task)
    {
        await dbContext.Tasks.AddAsync(task);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        dbContext.Tasks.Remove(new TaskEntity { Id = id });
        await dbContext.SaveChangesAsync();
    }
}