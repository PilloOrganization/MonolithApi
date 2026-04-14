using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IMedicineRepository
    {
        Task CreateAsync(Medicine medicine);
        Task DeleteAsync(Guid key);
    }
}
