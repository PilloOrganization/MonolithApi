using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IMedicineRepository
    {
        Task<Medicine> GetAsync(Guid key);
        void Create(Medicine medicine);
        Task DeleteAsync(Guid key);
    }
}
