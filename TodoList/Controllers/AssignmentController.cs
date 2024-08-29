using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.Entity;
using TodoList.Domain.ViewModels;
using TodoList.Service.TaskService;

namespace TodoList.Controllers;

[Route("api/task")]
public class AssignmentController(IAssignmentService assignmentService) : Controller
{
    [Route("getAll")]
    public async Task<List<AssignmentSiteDto>> GetAll()
    {
        return await assignmentService.GetAll();
    }

    [Route("create")]
    [HttpPost]
    public async Task Create([FromBody] AssignmentSiteDto assignment)
    {
        await assignmentService.Create(assignment);        
    }

    [Route("delete/{id}")]
    public async Task Delete(Guid id)
    {
        await assignmentService.Delete(id);
    }
}
