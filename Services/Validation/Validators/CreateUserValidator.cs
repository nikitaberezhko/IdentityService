using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.RoleId)
            .GreaterThanOrEqualTo(1)
            .LessThan(3)
            .NotEmpty();

        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Phone)
            .NotEmpty()
            .Must(x => !x.Any(c => !char.IsDigit(c) && c != '+'));
    }
}