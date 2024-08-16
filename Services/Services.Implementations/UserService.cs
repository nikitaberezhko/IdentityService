using AutoMapper;
using Domain;
using Exceptions.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Services.Auth.Interfaces;
using Services.Models.Request;
using Services.Models.Response;
using Services.Repositories.Interfaces;
using Services.Services.Interfaces;
using Services.Validation;

namespace Services.Services.Implementations;

public class UserService(
    IMapper mapper,
    IUserRepository userRepository,
    IJwtProvider jwtProvider,
    IPasswordHasher passwordHasher,
    UserValidator userValidator) : IUserService
{
    public async Task<Guid> Create(CreateUserModel model)
    {
        await userValidator.ValidateAsync(model);
        
        var user = mapper.Map<User>(model);
        user.Password = passwordHasher.GenerateHash(user.Password);
        
        return await userRepository.AddAsync(user);
    }

    public async Task<string> Authenticate(AuthenticateUserModel model)
    {
        await userValidator.ValidateAsync(model);

        var user = mapper.Map<User>(model);
        var result = await userRepository.GetByEmailAsync(user);
        
        if (passwordHasher.VerifyHash(
                password: user.Password, 
                hash: result.Password) == false)
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
        await userValidator.ValidateAsync(model);

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
        await userValidator.ValidateAsync(model);
        
        var user = await userRepository.DeleteAsync(mapper.Map<User>(model));
        var result = mapper.Map<UserModel>(user);
        
        return result;
    }
}