using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetByUserKeyAsync(Guid userKey);
        Task<Account> GetAsync(Guid key);
        Task<Account> GetWithRelatedAsync(Guid key);
        void Create(Account account);
        void Delete(Account account);
    }
}
