namespace TodoList.Domain.Entity;

public class Employee
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class EmployeeSiteDto
{
    public string Name { get; set; } = string.Empty;
}