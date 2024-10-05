using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.Entity;
using TodoList.Filers;
using TodoList.Service.TaskService;

namespace TodoList.Controllers;

[Route("api/task")]
[ApiController]
public class AssignmentController(IAssignmentService assignmentService, AssignmentControllerValidator validator)
    : Controller
{
    [Route("getAll")]
    public async Task<List<AssignmentSiteDto>> GetAll()
    {
        return await assignmentService.GetAll();
    }

    [Route("create")]
    [HttpPost]
    public async Task Create(AssignmentSiteDto assignment)
    {
        await assignmentService.Create(assignment);
    }

    [Route("delete/{id:guid}")]
    public async Task Delete(Guid id)
    {
        await assignmentService.Delete(id);
    }

    [Route("edit")]
    public async Task Edit(AssignmentSiteDto assignment)
    {
        await assignmentService.Edit(assignment);
    }
}