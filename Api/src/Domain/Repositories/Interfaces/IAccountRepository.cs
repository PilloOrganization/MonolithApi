using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAccountsByUserIdAsync(long userId);
        void Create(Account account);
        Task DeleteAsync(Guid key);
        void Delete(Account account);
    }
}
