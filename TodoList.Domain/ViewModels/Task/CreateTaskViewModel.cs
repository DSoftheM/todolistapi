using System.ComponentModel.DataAnnotations;
using TodoList.Domain.Enum;

namespace TodoList.Domain.ViewModels;

public class CreateTaskViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
}