using System.ComponentModel.DataAnnotations;

namespace Tastemap.Core.Entities.UserEntities;

public class UserDetails
{
    [Key]
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string? ProfileImageUrl { get; set; }
    public User User { get; set; }
    
    
}