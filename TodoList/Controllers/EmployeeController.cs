using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.Entity;
using TodoList.Service.EmployeeService;

namespace TodoList.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController(EmployeeService employeeService) : Controller
{
    [Route("create")]
    [HttpPost]
    public Task CreateEmployee(EmployeeSiteDto employeeSiteDto)
    {
        return employeeService.Create(employeeSiteDto);
    }
    
    [Route("delete/{id}")]
    public Task CreateEmployee(Guid id)
    {
        return employeeService.Delete(id);
    }

    [Route("update")]
    public Task UpdateEmployee(EmployeeSiteDto employeeSiteDto)
    {
        return employeeService.Update(employeeSiteDto);
    }

    [Route("getAll")]
    public async Task<List<EmployeeSiteDto>> GetAll()
    {
        return await employeeService.GetAll();
    }
}