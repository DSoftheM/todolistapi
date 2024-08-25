using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.Entity;
using TodoList.Domain.ViewModels;
using TodoList.Service.TaskService;

namespace TodoList.Controllers;

[Route("api/task")]
public class TaskController(ITaskService taskService) : Controller
{
    [Route("getAll")]
    public async Task<List<TaskEntity>> GetAll()
    {
        return await taskService.GetAll();
    }

    [Route("create")]
    [HttpPost]
    public async Task Create([FromBody] TaskEntitySiteDto task)
    {
        await taskService.Create(task);        
    }

    [Route("delete/{id}")]
    public async Task Delete(Guid id)
    {
        await taskService.Delete(id);
    }
}
