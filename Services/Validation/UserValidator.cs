using Exceptions.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Services.Models.Request;

namespace Services.Validation;

public class UserValidator(
    IValidator<CreateUserModel> createUserValidator,
    IValidator<DeleteUserModel> deleteUserValidator,
    IValidator<AuthorizeUserModel> authorizeUserValidator,
    IValidator<AuthenticateUserModel> authenticateUserValidator)
{
    public async Task ValidateAsync(CreateUserModel model)
    {
        var validationResult = await createUserValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartErrorMessage();
    }
    
    public async Task ValidateAsync(DeleteUserModel model)
    {
        var validationResult = await deleteUserValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartErrorMessage();
    }
    
    public async Task ValidateAsync(AuthorizeUserModel model)
    {
        var validationResult = await authorizeUserValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartErrorMessage();
    }
    
    public async Task ValidateAsync(AuthenticateUserModel model)
    {
        var validationResult = await authenticateUserValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartErrorMessage();
    }

    private void ThrowWithStandartErrorMessage()
    {
        throw new ServiceException
        {
            Title = "Model invalid",
            Message = "Model validation failed",
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}