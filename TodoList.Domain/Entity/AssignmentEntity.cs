namespace TodoList.Domain.Entity;

public class AssignmentEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.Now.ToUniversalTime();

    public Employee Employee { get; set; }
    public Guid EmployeeId { get; set; }
}

public class AssignmentEntitySiteDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public EmployeeSiteDto Employee { get; set; }
}

public static class AssignmentConverterExtension
{
    public static AssignmentEntitySiteDto ToSiteDto(this AssignmentEntity assignmentEntity)
    {
        return new AssignmentEntitySiteDto()
        {
            Title = assignmentEntity.Title, Text = assignmentEntity.Text,
            Employee = assignmentEntity.Employee.ToSiteDto(), Id = assignmentEntity.Id
        };
    }
}