using TodoList.Domain.Entity;

namespace TodoList.Service.EmployeeService;

public interface IEmployeeService
{
    public Task Create(EmployeeSiteDto employee);
    public Task<List<Employee>> GetAll();
    public Task Delete(Guid id);
}