using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCoursesByAccountIdAsync(long accountId);
        void Create(Course course);
        void Delete(Course course);
    }
}
