using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class DeleteUserValidator : AbstractValidator<DeleteUserModel>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}