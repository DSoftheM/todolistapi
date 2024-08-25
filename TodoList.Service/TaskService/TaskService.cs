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

    public async Task Create(TaskEntitySiteDto task)
    {
        await dbContext.Tasks.AddAsync(new TaskEntity() { Text = task.Text, Title = task.Title });
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var assigment = await dbContext.Tasks.FindAsync(id);
        if (assigment != null) dbContext.Tasks.Remove(assigment);
        await dbContext.SaveChangesAsync();
    }
}