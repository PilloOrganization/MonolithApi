using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task CreateAsync(Course course);
        Task DeleteAsync(Guid key);
    }
}
