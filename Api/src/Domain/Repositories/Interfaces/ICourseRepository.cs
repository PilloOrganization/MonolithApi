using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetByAccountIdAsync(long accountId);
        Task<List<Course>> GetByAccountKeyAsync(Guid accountKey);
        Task<Course> GetAsync(long id);
        Task<Course> GetAsync(Guid courseKey);
        Task<long> GetIdAsync(Guid courseKey);
        void Create(Course course);
        void Delete(Course course);
    }
}
