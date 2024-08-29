namespace TodoList.Domain.Entity;

public class Assignment
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.Now.ToUniversalTime();

    public Employee? Employee { get; set; }
    public Guid? EmployeeId { get; set; }
}

public class AssignmentSiteDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public EmployeeSiteDto? Employee { get; set; }
    public DateTime Created { get; set; } 
}

public static class AssignmentConverterExtension
{
    public static AssignmentSiteDto ToSiteDto(this Assignment assignment)
    {
        return new AssignmentSiteDto()
        {
            Title = assignment.Title, Text = assignment.Text,
            Employee = assignment.Employee?.ToSiteDto(), Id = assignment.Id,
            Created = assignment.Created
        };
    }
}