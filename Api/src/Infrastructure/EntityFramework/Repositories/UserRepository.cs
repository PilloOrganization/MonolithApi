using Domain.Models;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.EntityFramework.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlServerAppDbContext _context;

        public UserRepository(SqlServerAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<long> GetIdByKeyAsync(Guid key)
        {
            return _context.Users.Where(e => e.EntityKey == key).Select(e => e.Id).SingleAsync();
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return GetByProperty(u => u.Email == email);
        }

        public Task<User?> GetByPhoneAsync(string phone)
        {
            return GetByProperty(u => u.PhoneNumber == phone);
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            return GetByProperty(u => u.Username == username);
        }

        private Task<User?> GetByProperty(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.Include(u => u.Accounts).FirstOrDefaultAsync(predicate);
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
        }

        //public Task DeleteUserAsync(Guid key)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteUserAsync(long id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
