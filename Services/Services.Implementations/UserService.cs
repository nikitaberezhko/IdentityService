using AutoMapper;
using Domain;
using Exceptions.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Services.Auth.Interfaces;
using Services.Models.Request;
using Services.Models.Response;
using Services.Repositories.Interfaces;
using Services.Services.Interfaces;

namespace Services.Services.Implementations;

public class UserService(
    IMapper mapper,
    IUserRepository userRepository,
    IJwtProvider jwtProvider,
    IPasswordHasher passwordHasher,
    IValidator<CreateUserModel> createUserValidator,
    IValidator<DeleteUserModel> deleteUserValidator,
    IValidator<AuthorizeUserModel> authorizeUserValidator,
    IValidator<AuthenticateUserModel> authenticateUserValidator) : IUserService
{
    public async Task<Guid> Create(CreateUserModel model)
    {
        var validationResult = await createUserValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation failed",
                Message = $"User with this fields failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };

        var user = mapper.Map<User>(model);
        
        user.Password = passwordHasher.GenerateHash(user.Password);
        
        return await userRepository.AddAsync(user);
    }

    public async Task<string> Authenticate(AuthenticateUserModel model)
    {
        var validationResult = await authenticateUserValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation failed",
                Message = $"User with this fields failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };

        var user = mapper.Map<User>(model);
        var result = await userRepository.GetByEmailAsync(user);
        
        if (passwordHasher.VerifyHash(user.Password, result.Password) == false)
            throw new ServiceException
            {
                Title = "Authentication failed",
                Message = "Wrong login or password",
                StatusCode = StatusCodes.Status401Unauthorized
            };
        
        var token = jwtProvider.GenerateToken(result);
        return token;
    }

    public async Task<UserModel> Authorize(AuthorizeUserModel model)
    {
        var validationResult = await authorizeUserValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation failed",
                Message = $"User with this fields failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };

        var user = jwtProvider.VerifyToken(model.Token);
        
        var result = new UserModel
        {
            Id = user.Id,
            RoleId = user.RoleId
        };
        return result;
    }

    public async Task<UserModel> Delete(DeleteUserModel model)
    {
        var validationResult = await deleteUserValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Validation failed",
                Message = $"User with this fields failed validation",
                StatusCode = StatusCodes.Status400BadRequest
            };

        var user = await userRepository.DeleteAsync(mapper.Map<User>(model));

        var result = mapper.Map<UserModel>(user);
        return result;
    }
}