using Tastemap.Core.Entities.UserEntities;

namespace Tastemap.Core.Repositories.UserRepositories;

public interface IUserDetailsRepository
{
    Task<List<UserDetails>> GetAllUserDetailsAsync();
    Task<UserDetails> GetUserDetails(Guid userId);
    Task<UserDetails> GetUserDetailsByUsername(string username);
    Task<UserDetails> GetUserDetailsByEmail(string email);
    Task AddAsync(UserDetails userDetails);
    Task<bool> UpdateAsync(UserDetails userDetails);
    
}