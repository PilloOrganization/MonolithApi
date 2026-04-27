using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByPhoneAsync(string phone);
        Task<User?> GetByUsernameAsync(string username);
        Task<long> GetIdAsync(Guid key);
        void Create(User user);
    }
}
