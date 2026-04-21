using Microsoft.EntityFrameworkCore;
using Tastemap.Core.Entities.UserEntities;
using Tastemap.Core.Repositories.UserRepositories;
using Tastemap.Repository.DbContext;

namespace Tastemap.Repository.Repositories.UserRepositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByUsernameAsync(string username)
        => await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);

    public async Task<bool> ExistByEmailAsync(string email)
        => await _context.Users.AnyAsync(u => u.Email == email);

    public async Task<bool> ExistByUsernameAsync(string username)
        => await _context.Users.AnyAsync(u => u.Username == username);

    public async Task AddAsync(User user)
        => await _context.Users.AddAsync(user);

    public async Task SaveChangesAsync() 
        => await _context.SaveChangesAsync();
}