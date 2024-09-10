namespace TodoList.Domain.Entity;

public class Assignment
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.Now.ToUniversalTime();

    public List<Employee> Employees { get; set; } = [];
    public List<Guid> EmployeesIds { get; set; } = [];
}

public class AssignmentSiteDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public List<EmployeeSiteDto> Employees { get; set; } = [];
    public DateTime Created { get; set; }
}

public static class AssignmentConverterExtension
{
    public static AssignmentSiteDto ToSiteDto(this Assignment assignment)
    {
        return new AssignmentSiteDto()
        {
            Title = assignment.Title, Text = assignment.Text,
            Employees = assignment.Employees.Select(x => x.ToSiteDto()).ToList(), Id = assignment.Id,
            Created = assignment.Created
        };
    }
}