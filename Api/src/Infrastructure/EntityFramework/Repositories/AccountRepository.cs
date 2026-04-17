using Domain.Models;
using Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Task CreateAsync(Account account)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> GetAccountsByUserIdAsync(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
