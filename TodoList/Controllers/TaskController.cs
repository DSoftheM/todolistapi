using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.Entity;
using TodoList.Domain.Response;
using TodoList.Domain.ViewModels;
using TodoList.Service.Interfaces;

namespace TodoList;

[Route("api/task")]
class TaskController : Controller
{
    [HttpPost]
    [Route("create")]
    public async Task CreateTask([FromBody] CreateTaskViewModel taskDto)
    {
        return;
    }
}
