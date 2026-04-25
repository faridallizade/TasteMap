using FluentValidation;
using Tastemap.Core.DTOs.Auth;

namespace Tastemap.Service.Validations.Auth;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x=>x.FirstName)
            .NotEmpty().WithMessage("First Name is required")
            .MinimumLength(3).MaximumLength(30)
            .WithMessage("First name must be between 3 and 30 characters long");
        
        RuleFor(x=>x.LastName)
            .NotEmpty().WithMessage("Last Name is required")
            .MinimumLength(3).MaximumLength(30).WithMessage("Last name must be between 3 and 30 characters long");
        
        RuleFor(x=>x.Password)
            .NotEmpty().MinimumLength(8).WithMessage("Password must be at least 8 characters long");
        
        RuleFor(x=>x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches(@"^\+994(50|51|55|70|77|10|99)\d{7}$")
            .WithMessage("Phone number format doesn't match with example (+994XXXXXXXXX).");

    }
}