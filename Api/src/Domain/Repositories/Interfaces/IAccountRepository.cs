using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task CreateAsync(Account account);
        Task<IEnumerable<Account>> GetAccountsByUserIdAsync(long userId);
        Task DeleteAsync(Guid key);
    }
}
