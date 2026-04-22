using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetByAccountIdAsync(long accountId);
        Task<IEnumerable<Course>> GetByAccountKeyAsync(Guid accountKey);
        Task<long> GetIdAsync(Guid courseKey);
        Task<Course> GetAsync(Guid courseKey);
        void Create(Course course);
        void Delete(Course course);
    }
}
