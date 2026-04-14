using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IDoseRepository
    {
        Task DeleteAsync(Guid key);
        Task UpdateIsTakenAsync(Guid key, bool isTaken);
        Task BulkCreateAsync(IEnumerable<Dose> doses);
    }
}
