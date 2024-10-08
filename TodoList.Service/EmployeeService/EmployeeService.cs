﻿using Microsoft.EntityFrameworkCore;
using Todolist.DAL;
using TodoList.Domain.Entity;

namespace TodoList.Service.EmployeeService;

public class EmployeeService(AppDbContext dbContext)
{
    public async Task Create(EmployeeSiteDto employee)
    {
        await dbContext.Employees.AddAsync(new Employee() { Name = employee.Name });
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<EmployeeSiteDto>> GetAll()
    {
        return await dbContext.Employees.AsNoTracking().Select(x => x.ToSiteDto()).ToListAsync();
    }

    public async Task Delete(Guid id)
    {
        var employee = await dbContext.Employees.Include(x => x.Tasks).FirstOrDefaultAsync(x => x.Id == id);
        if (employee == null) return;

        employee.Tasks.ForEach(t => t.Employees.Remove(employee));
        dbContext.Employees.Remove(employee);

        await dbContext.SaveChangesAsync();
    }

    public async Task Update(EmployeeSiteDto employee)
    {
        var model = dbContext.Employees.FirstOrDefault(x => x.Id == employee.Id);
        if (model == null) return;

        model.Name = employee.Name;

        dbContext.Employees.Update(model);
        await dbContext.SaveChangesAsync();
    }
}