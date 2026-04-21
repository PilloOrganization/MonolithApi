using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetByAccountIdAsync(long accountId);
        Task<Course> GetAsync(Guid courseKey);
        void Create(Course course);
        void Delete(Course course);
    }
}
