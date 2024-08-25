namespace Todolist.DAL.Models;

public class CourseEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    
    // One-to-many
    public List<LessonEntity> Lessons { get; set; } = [];
    
    // One-to-one
    public Guid AuthorId { get; set; }
    public AuthorEntity? Author { get; set; }
    
    // Many-to-many
    public List<StudentEntity> Students { get; set; } = [];
}