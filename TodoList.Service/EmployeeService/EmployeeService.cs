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
            if (employee.Task != null)
                employee.Task.EmployeeId = null;

            dbContext.Employees.Remove(employee);
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task Update(EmployeeSiteDto employee)
    {
        var task = await dbContext.Tasks.FirstOrDefaultAsync(x => x.EmployeeId == employee.Id);
        
        dbContext.Employees.Update(employee.ToDataDto(task?.Id));
        await dbContext.SaveChangesAsync();
    }
}