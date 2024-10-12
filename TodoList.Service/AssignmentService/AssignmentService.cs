using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Todolist.DAL;
using TodoList.Domain.Entity;
using TodoList.Domain.Enum;

namespace TodoList.Service.TaskService;

public class AssignmentService(AppDbContext dbContext)
{
    public async Task<List<AssignmentSiteDto>> GetAll(string term, FilterBy filterBy)
    {
        term = term.ToLower();

        return await dbContext.Tasks.AsNoTracking().Where(t => t.Text.Contains(term) || t.Title.Contains(term))
            .Include(x => x.Employees)
            .OrderByDescending(x => x.Created)
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
            Employees = employees, Priority = assignment.Priority
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
        model.Priority = assignment.Priority;

        var ids = assignment.Employees.Select(x => x.Id).ToList();
        var employees = await dbContext.Employees.Where(x => ids.Contains(x.Id)).ToListAsync();
        model.Employees = employees;

        dbContext.Employees.UpdateRange(employees);
        dbContext.Tasks.Update(model);
        await dbContext.SaveChangesAsync();
    }
}

file class AssignmentComparer(FilterBy filterBy) : IComparer<Assignment>
{
    public int Compare(Assignment? x, Assignment? y)
    {
        return filterBy switch
        {
            FilterBy.Priority => x?.Priority.CompareTo(y?.Priority) ?? 0,
            FilterBy.CreationDate => x?.Created.CompareTo(y?.Created) ?? 0,
            _ => 0
        };
    }
}