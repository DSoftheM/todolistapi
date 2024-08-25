using Todolist.DAL.Models;

namespace TodoList.Service.CourseService;

public interface ICourseService
{
    public Task<List<CourseEntity>> GetALl();
    public Task<List<CourseEntity>> GetByFilter(string title, decimal? price);
    public Task<List<CourseEntity>> GetByPage(int pageNumber, int pageSize);
    public Task Add(CourseEntity course);
    public Task Update(CourseEntity course);
    public Task Delete(CourseEntity course);
    public Task<List<CourseEntity>> GetALlWithLessons();
    public Task<CourseEntity?> GetById(Guid id);
}