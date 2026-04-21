using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetByUserIdAsync(long userId);
        Task<Account> GetByKeyAsync(Guid key);
        void Create(Account account);
        Task DeleteAsync(Guid key);
        void Delete(Account account);
    }
}
