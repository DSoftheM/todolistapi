namespace TodoList.Domain.Entity;

public class TaskEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.Now.ToUniversalTime();
}

public class TaskEntitySiteDto
{
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}