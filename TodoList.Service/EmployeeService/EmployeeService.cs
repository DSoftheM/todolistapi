using Microsoft.EntityFrameworkCore;
using Todolist.DAL;
using TodoList.Domain.Entity;

namespace TodoList.Service.EmployeeService;

public class EmployeeService(AppDbContext dbContext) : IEmployeeService
{
    public async Task Create(EmployeeSiteDto employee)
    {
        await dbContext.Employees.AddAsync(new Employee() { Name = employee.Name });
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Employee>> GetAll()
    {
        return await dbContext.Employees.AsNoTracking().ToListAsync();
    }

    public async Task Delete(Guid id)
    {
        var employee = await dbContext.Employees.Include(x => x.Task).FirstOrDefaultAsync(x => x.Id == id);
        if (employee != null)
        {
            employee.Task?.Employees.Remove(employee);
            dbContext.Employees.Remove(employee);
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task Update(EmployeeSiteDto employee)
    {
        var task = await dbContext.Tasks.FirstOrDefaultAsync(x => x.EmployeesIds.Contains(employee.Id));
        var model = new Employee() { Name = employee.Name, TaskId = task?.Id };
        
        dbContext.Employees.Update(model);
        await dbContext.SaveChangesAsync();
    }
}