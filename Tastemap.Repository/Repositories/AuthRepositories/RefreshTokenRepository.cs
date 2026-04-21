using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Tastemap.Core.Entities.Authentication;
using Tastemap.Core.Repositories.AuthRepositories;
using Tastemap.Repository.DbContext;

namespace Tastemap.Repository.Repositories.AuthRepositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByRefreshToken(string refreshToken)
        => await _context.RefreshTokens
            .Include(rt => rt.User)
            .ThenInclude(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(r => r.Token == refreshToken);

    public async Task AddAsync(RefreshToken refreshToken)
        => await _context.RefreshTokens.AddAsync(refreshToken);

    public async Task RevoveAsync(RefreshToken refreshToken)
    {
        refreshToken.IsRevoked = true;
        _context.RefreshTokens.Update(refreshToken);
    }

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}