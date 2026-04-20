using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IDoseRepository
    {
        void BulkCreate(IEnumerable<Dose> doses);
        Task UpdateIsTakenAsync(Guid key, bool isTaken);
        Task DeleteAsync(Guid key);
    }
}
