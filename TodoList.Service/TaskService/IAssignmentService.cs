using TodoList.Domain.Entity;

namespace TodoList.Service.TaskService;

public interface IAssignmentService
{
    public Task<List<AssignmentEntitySiteDto>> GetAll();
    public Task Create(AssignmentEntitySiteDto assignment);
    public Task Delete(Guid id);
}