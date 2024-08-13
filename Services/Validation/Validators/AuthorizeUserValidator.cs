using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class AuthorizeUserValidator : AbstractValidator<AuthorizeUserModel>
{
    public AuthorizeUserValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
    }
}