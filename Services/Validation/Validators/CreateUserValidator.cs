using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.Password).NotEmpty();

        RuleFor(x => x.RoleId)
            .GreaterThanOrEqualTo(1)
            .LessThan(3)
            .NotEmpty();

        RuleFor(x => x.Name).NotEmpty();
    }
}