using TodoList.Domain.Entity;

namespace TodoList.Service.TaskService;

public interface ITaskService
{
    public Task<List<TaskEntity>> GetAll();
    public Task Create(TaskEntitySiteDto task);
    public Task Delete(Guid id);
}