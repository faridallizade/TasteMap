using Tastemap.Core.Entities.UserEntities;
using Tastemap.Core.Repositories.UserRepositories;
using Tastemap.Repository.DbContext;

namespace Tastemap.Repository.Repositories.UserRepositories;

public class UserDetailsRepository : IUserDetailsRepository
{
    private readonly AppDbContext _context;

    public UserDetailsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserDetails>> GetAllUserDetailsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<UserDetails> GetUserDetails(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDetails> GetUserDetailsByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDetails> GetUserDetailsByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(UserDetails userDetails)
        => await _context.UserDetails.AddAsync(userDetails);

    public async Task<bool> UpdateAsync(UserDetails userDetails)
    {
        throw new NotImplementedException();
    }
}