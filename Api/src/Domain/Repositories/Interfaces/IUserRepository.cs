using Domain.Models;

namespace Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task DeleteUserAsync(Guid key);
        Task DeleteUserAsync(long id);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByPhoneAsync(string phone);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
