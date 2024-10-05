using FluentValidation;
using TodoList.Domain.Entity;

namespace TodoList.Controllers;

public class AssignmentControllerValidator: AbstractValidator<AssignmentSiteDto>
{
    public AssignmentControllerValidator()
    {
        RuleFor(x => x.Employees).NotEmpty();
        RuleFor(x => x.Text).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
    }
}