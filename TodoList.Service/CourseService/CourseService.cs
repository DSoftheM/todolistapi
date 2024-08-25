using Todolist.DAL.Models;

namespace TodoList.Service.CourseService;

public class CourseService(): ICourseService
{
    public Task<List<CourseEntity>> GetALl()
    {
        throw new NotImplementedException();
    }

    public Task<List<CourseEntity>> GetByFilter(string title, decimal? price)
    {
        throw new NotImplementedException();
    }

    public Task<List<CourseEntity>> GetByPage(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task Add(CourseEntity course)
    {
        throw new NotImplementedException();
    }

    public Task Update(CourseEntity course)
    {
        throw new NotImplementedException();
    }

    public Task Delete(CourseEntity course)
    {
        throw new NotImplementedException();
    }

    public Task<List<CourseEntity>> GetALlWithLessons()
    {
        throw new NotImplementedException();
    }

    public Task<CourseEntity?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}