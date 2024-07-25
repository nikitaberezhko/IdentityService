using FluentValidation;
using Services.Models.Request;

namespace Services.Validation;

public class CreateUserValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.Password).NotEmpty();

        RuleFor(x => x.RoleId).NotEmpty();

        RuleFor(x => x.Name).NotEmpty();
    }
}