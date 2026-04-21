using Tastemap.Core.Entities.Authentication;

namespace Tastemap.Core.Repositories.AuthRepositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByRefreshToken(string refreshToken);
    Task AddAsync(RefreshToken refreshToken);
    Task RevoveAsync(RefreshToken refreshToken);
    Task SaveChangesAsync();
    
}