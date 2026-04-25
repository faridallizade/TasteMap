using FluentValidation;
using Tastemap.Core.DTOs.UserDetails;

namespace Tastemap.Service.Validations.UserDetails;

public class CreateUserDetailsDtoValidator : AbstractValidator<CreateUserDetailsDto>
{
    public CreateUserDetailsDtoValidator()
    {
        RuleFor(x=>x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required")
            .MinimumLength(3).WithMessage("First name is too short (min. 3 characters)")
            .MaximumLength(30).WithMessage("First name is too long (max. 30 characters)")
            .Matches(@"^[a-zA-ZəıöğçşəİDefault]*$").WithMessage("First name must be contains  just letters");
        
        RuleFor(x=>x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required")
            .MinimumLength(3).WithMessage("Last name is too short (min. 3 characters)")
            .MaximumLength(30).WithMessage("Last name is too long (max. 30 characters)")
            .Matches(@"^[a-zA-ZəıöğçşəİDefault]*$").WithMessage("Last name must be contains  just letters");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches(@"^\+994(50|51|55|70|77|10|99)\d{7}$")
            .WithMessage("Phone number format doesn't match with example (+994XXXXXXXXX).");
        
        RuleFor(x => x.ProfileImageUrl)
            .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Invalid profile image url");
    }
}