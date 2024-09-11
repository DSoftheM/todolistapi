using Microsoft.EntityFrameworkCore;
using Todolist.DAL;
using TodoList.Domain.Entity;

namespace TodoList.Service.TaskService;

public class AssignmentService(AppDbContext dbContext) : IAssignmentService
{
    public async Task<List<AssignmentSiteDto>> GetAll()
    {
        return await dbContext.Tasks.AsNoTracking().Include(x => x.Employees)
            .Select(x => x.ToSiteDto())
            .ToListAsync();
    }

    public async Task Create(AssignmentSiteDto assignment)
    {
        var employeesIds = assignment.Employees.Select(x => x.Id).ToList();
        var employees = await dbContext.Employees.Where(e => employeesIds.Contains(e.Id)).ToListAsync();

        var newAssignment = new Assignment()
        {
            Text = assignment.Text, Title = assignment.Title,
            Employees = employees
        };

        await dbContext.Tasks.AddAsync(newAssignment);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var assigment = await dbContext.Tasks.FindAsync(id);
        if (assigment != null) dbContext.Tasks.Remove(assigment);
        await dbContext.SaveChangesAsync();
    }

    public async Task Edit(AssignmentSiteDto assignment)
    {
        var model = await dbContext.Tasks.FindAsync(assignment.Id);
        if (model == null) return;

        model.Text = assignment.Text;
        model.Title = assignment.Title;

        dbContext.Tasks.Update(model);
        await dbContext.SaveChangesAsync();
    }
}