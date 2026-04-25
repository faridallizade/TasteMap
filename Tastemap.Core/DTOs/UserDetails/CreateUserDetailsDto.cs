namespace Tastemap.Core.DTOs.UserDetails;

public class CreateUserDetailsDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string? ProfileImageUrl { get; set; }
}