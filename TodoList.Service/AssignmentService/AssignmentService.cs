using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Todolist.DAL;
using TodoList.Domain.Entity;

namespace TodoList.Service.TaskService;

public class AssignmentService(AppDbContext dbContext) 
{
    public async Task<List<AssignmentSiteDto>> GetAll(string term)
    {
        term = term.ToLower();
        
        return await dbContext.Tasks.AsNoTracking().Where(Predicate()).Include(x => x.Employees)
            .Select(x => x.ToSiteDto())
            .ToListAsync();

        Expression<Func<Assignment, bool>> Predicate()
        {
            return t => t.Text.Contains(term) || t.Title.Contains(term);
        }
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
        var model = await dbContext.Tasks.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == assignment.Id);
        if (model == null) return;

        model.Text = assignment.Text;
        model.Title = assignment.Title;
        model.Done = assignment.Done;

        var ids = assignment.Employees.Select(x => x.Id).ToList();
        var employees = await dbContext.Employees.Where(x => ids.Contains(x.Id)).ToListAsync();
        model.Employees = employees;

        dbContext.Employees.UpdateRange(employees);
        dbContext.Tasks.Update(model);
        await dbContext.SaveChangesAsync();
    }
}