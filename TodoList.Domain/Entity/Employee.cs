namespace TodoList.Domain.Entity;

public class Employee
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public AssignmentEntity? Task { get; set; }
    public Guid? TaskId { get; set; }
}

public class EmployeeSiteDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public static class EmployeeConverterExtension
{
    public static EmployeeSiteDto ToSiteDto(this Employee employee)
    {
        return new EmployeeSiteDto() { Id = employee.Id, Name = employee.Name, };
    }
}