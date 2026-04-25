using Tastemap.Core.Repositories.AuthRepositories;
using Tastemap.Core.Repositories.UserRepositories;

namespace Tastemap.Core.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    IUserDetailsRepository UserDetails { get; }
    Task<int> SaveChangesAsync();
}