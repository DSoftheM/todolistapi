using TodoList.Domain.Entity;

namespace TodoList.Service.TaskService;

public interface IAssignmentService
{
    public Task<List<AssignmentSiteDto>> GetAll();
    public Task Create(AssignmentSiteDto assignment);
    public Task Edit(AssignmentSiteDto assignment);
    public Task Delete(Guid id);
}