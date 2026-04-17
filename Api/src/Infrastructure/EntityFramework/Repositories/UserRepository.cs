using Domain.Models;
using Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(Guid key)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByPhoneAsync(string phone)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
