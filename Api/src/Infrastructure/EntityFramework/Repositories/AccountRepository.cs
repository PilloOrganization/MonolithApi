using Domain.Models;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SqlServerAppDbContext _context;

        public AccountRepository(SqlServerAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetByUserIdAsync(long userId)
        {
            return await _context.Accounts.Where(e => e.UserId == userId).ToListAsync();
        }

        public async Task<Account> GetByKeyAsync(Guid key)
        {
            return await _context.Accounts.SingleAsync(e => e.EntityKey == key);
        }

        public void Create(Account account)
        {
            _context.Accounts.Add(account);
        }

        public void Delete(Account account)
        {
            _context.Accounts.Remove(account);
        }

        public async Task DeleteAsync(Guid key)
        {
            await _context.Accounts.Where(e => e.EntityKey == key).ExecuteDeleteAsync();
        }
    }
}
