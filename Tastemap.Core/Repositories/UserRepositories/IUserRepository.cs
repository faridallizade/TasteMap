using Tastemap.Core.Entities.UserEntities;

namespace Tastemap.Core.Repositories.UserRepositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> ExistByEmailAsync(string email);
    Task<bool> ExistByUsernameAsync(string username);
    Task AddAsync(User user);
    Task SaveChangesAsync();
}