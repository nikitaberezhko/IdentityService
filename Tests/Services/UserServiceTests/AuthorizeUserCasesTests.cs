using AutoMapper;
using Domain;
using Exceptions.Contracts.Infrastructure;
using Moq;
using Services.Auth.Interfaces;
using Services.Models.Request;
using Services.Repositories.Interfaces;
using Services.Services.Implementations;
using Services.Validation;
using Xunit;

namespace Tests.Services.UserServiceTests;

public class AuthorizeUserCasesTests
{
    [Fact]
    public async Task AuthorizeCase_Should_Return_Right_Id_And_RoleId()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "somename",
            Email = "good@mail.com",
            Password = "password",
            Phone = "+123456789",
            RoleId = 1,
            IsDeleted = false
        };
        var token = Provider.Get<IJwtProvider>().GenerateToken(user);
        var model = new AuthorizeUserModel { Token = token };
        var service = new UserService(Provider.Get<IMapper>(),
            new Mock<IUserRepository>().Object,
            Provider.Get<IJwtProvider>(),
            Provider.Get<IPasswordHasher>(),
            Provider.Get<UserValidator>());
        
        // Act
        var result = await service.Authorize(model);
        
        // Assert
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.RoleId, result.RoleId);
    }
    
    [Fact]
    public async Task AuthorizeCase_Should_Throw_InfrastructureException_If_Token_Is_Invalid()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "somename",
            Email = "good@mail.com",
            Password = "password",
            Phone = "+123456789",
            RoleId = 1,
            IsDeleted = false
        };
        var token = Provider.Get<IJwtProvider>().GenerateToken(user);
        var badToken = token.Replace(token[^1], token[^2]);
        var model = new AuthorizeUserModel { Token = badToken };
        var service = new UserService(Provider.Get<IMapper>(),
            new Mock<IUserRepository>().Object,
            Provider.Get<IJwtProvider>(),
            Provider.Get<IPasswordHasher>(),
            Provider.Get<UserValidator>());
        
        // Act

        // Assert
        await Assert.ThrowsAsync<InfrastructureException>(async () => await service.Authorize(model));
    }
}