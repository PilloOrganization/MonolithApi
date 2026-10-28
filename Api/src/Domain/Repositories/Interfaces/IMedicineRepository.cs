using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IMedicineRepository
    {
        Task<IEnumerable<Medicine>> GetByUserIdAsync(long userId);
        Task<Medicine> GetAsync(Guid key);
        Task<Medicine?> GetAsync(string medicineName, long userId);
        void Create(Medicine medicine);
        Task DeleteAsync(Guid key);
    }
}
