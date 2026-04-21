using Tastemap.Core.Entities.RoleEntities;

namespace Tastemap.Core.Entities.UserEntities;

public class UserRole
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public User User { get; set; }
}