using TodoList.Domain.Entity;

namespace TodoList.Service.EmployeeService;

public interface IEmployeeService
{
    public Task Create(EmployeeSiteDto employee);
    public Task<List<EmployeeSiteDto>> GetAll();
    public Task Delete(Guid id);
    public Task Update(EmployeeSiteDto employee);
}