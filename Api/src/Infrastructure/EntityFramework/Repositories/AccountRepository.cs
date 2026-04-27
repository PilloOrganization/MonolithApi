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

        public async Task<IEnumerable<Account>> GetByUserKeyAsync(Guid userKey)
        {
            return await _context.Accounts.Where(e => e.User.EntityKey == userKey).ToListAsync();
        }

        public async Task<Account> GetAsync(Guid key)
        {
            return await _context.Accounts.SingleAsync(e => e.EntityKey == key);
        }

        public async Task<Account> GetWithRelatedAsync(Guid key)
        {
            return await _context.Accounts
                .Include(a => a.Courses)
                    .ThenInclude(e => e.PrescriptionSchedules)
                        .ThenInclude(e => e.Doses)
                .SingleAsync(e => e.EntityKey == key);
        }

        public void Create(Account account)
        {
            _context.Accounts.Add(account);
        }

        public void Delete(Account account)
        {
            _context.Accounts.Remove(account);
        }
    }
}
