using Microsoft.EntityFrameworkCore;
using Todolist.DAL;
using TodoList.Domain.Entity;

namespace TodoList.Service.TaskService;

public class AssignmentService(AppDbContext dbContext) : IAssignmentService
{
    public async Task<List<AssignmentSiteDto>> GetAll()
    {
        return await dbContext.Tasks.AsNoTracking().Include(x => x.Employee)
            .Select(x => x.ToSiteDto())
            .ToListAsync();
    }

    public async Task Create(AssignmentSiteDto assignment)
    {
        var createdAssignment = await dbContext.Tasks.AddAsync(new Assignment()
            { Text = assignment.Text, Title = assignment.Title, EmployeeId = assignment.Employee.Id });

        var employee = await dbContext.Employees.FindAsync(assignment.Employee.Id);
        if (employee == null) return;
        employee.TaskId = createdAssignment.Entity.Id;

        dbContext.Employees.Update(employee);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var assigment = await dbContext.Tasks.FindAsync(id);
        if (assigment != null) dbContext.Tasks.Remove(assigment);
        await dbContext.SaveChangesAsync();
    }
}