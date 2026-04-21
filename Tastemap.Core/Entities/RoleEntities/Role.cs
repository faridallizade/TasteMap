using Tastemap.Core.Entities.UserEntities;

namespace Tastemap.Core.Entities.RoleEntities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsFullAdmin { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } =  new List<UserRole>();
    
}