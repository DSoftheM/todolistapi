using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.Entity;
using TodoList.Service.EmployeeService;

namespace TodoList.Controllers;

[Route("api/[controller]")]
public class EmployeeController(IEmployeeService employeeService) : Controller
{
    [Route("create")]
    [HttpPost]
    public Task CreateEmployee([FromBody]EmployeeSiteDto employeeSiteDto)
    {
        return employeeService.Create(employeeSiteDto);
    }
    
    [Route("delete/{id}")]
    public Task CreateEmployee(Guid id)
    {
        return employeeService.Delete(id);
    }

    [Route("update")]
    public Task UpdateEmployee([FromBody] EmployeeSiteDto employeeSiteDto)
    {
        return employeeService.Update(employeeSiteDto);
    }

    [Route("getAll")]
    public async Task<List<Employee>> GetAll()
    {
        return await employeeService.GetAll();
    }
}