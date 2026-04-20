using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IMedicineRepository
    {
        void Create(Medicine medicine);
        Task DeleteAsync(Guid key);
    }
}
