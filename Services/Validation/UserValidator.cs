using Exceptions.Contracts.Services;
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
    public async Task<bool> ValidateAsync(CreateUserModel model)
    {
        var validationResult = await createUserValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartErrorMessage();
        
        return true;
    }
    
    public async Task<bool> ValidateAsync(DeleteUserModel model)
    {
        var validationResult = await deleteUserValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartErrorMessage();
        
        return true;
    }
    
    public async Task<bool> ValidateAsync(AuthorizeUserModel model)
    {
        var validationResult = await authorizeUserValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartErrorMessage();
        
        return true;
    }
    
    public async Task<bool> ValidateAsync(AuthenticateUserModel model)
    {
        var validationResult = await authenticateUserValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartErrorMessage();
        
        return true;
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