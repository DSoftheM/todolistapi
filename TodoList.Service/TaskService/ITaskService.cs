using TodoList.Domain.Entity;

namespace TodoList.Service.TaskService;

public interface ITaskService
{
    public Task<List<TaskEntity>> GetAll();
    public Task Create(TaskEntity task);
    public Task Delete(Guid id);
}