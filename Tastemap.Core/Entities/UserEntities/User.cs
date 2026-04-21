using Tastemap.Core.Entities.Authentication;
using Tastemap.Core.Enums;

namespace Tastemap.Core.Entities.UserEntities;

public class User : BaseEntity.BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public UserStatus Status { get; set; }
    public UserDetails? UserDetails { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } =  new List<UserRole>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } =  new List<RefreshToken>();
}