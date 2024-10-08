using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserModel>
{
    public AuthenticateUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}