using Microsoft.EntityFrameworkCore.Metadata;
using Tastemap.Core.Repositories;
using Tastemap.Core.Repositories.AuthRepositories;
using Tastemap.Core.Repositories.UserRepositories;
using Tastemap.Repository.DbContext;

namespace Tastemap.Repository.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IUserRepository Users { get; }
    public IRefreshTokenRepository RefreshTokens { get; }

    public UnitOfWork(AppDbContext context, IUserRepository users, IRefreshTokenRepository refreshTokens)
    {
        _context = context;
        Users = users;
        RefreshTokens = refreshTokens;
    }

    public async Task<int> SaveChangesAsync() 
        => await _context.SaveChangesAsync();


    public async void Dispose()
        => await _context.DisposeAsync();
}