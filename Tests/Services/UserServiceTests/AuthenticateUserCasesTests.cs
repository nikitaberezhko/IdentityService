using AutoMapper;
using Domain;
using Exceptions.Contracts.Services;
using Moq;
using Services.Auth.Interfaces;
using Services.Models.Request;
using Services.Repositories.Interfaces;
using Services.Services.Implementations;
using Services.Validation;
using Xunit;

namespace Tests.Services.UserServiceTests;

public class AuthenticateUserCasesTests
{
    [Fact]
    public async Task AuthenticateCase_Should_Return_Token()
    {
        // Arrange
        var password = "password";
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "somename",
            Email = "good@mail.com",
            Password = Provider.Get<IPasswordHasher>().GenerateHash(password),
            Phone = "+123456789",
            RoleId = 1,
            IsDeleted = false
        };
        var model = new AuthenticateUserModel
        {
            Email = user.Email,
            Password = password
        };
        var repository = new Mock<IUserRepository>();
        repository.Setup(x=> x.GetByEmailAsync(It.IsAny<User>())).ReturnsAsync(user);
        var service = new UserService(Provider.Get<IMapper>(),
            repository.Object,
            Provider.Get<IJwtProvider>(),
            Provider.Get<IPasswordHasher>(),
            Provider.Get<UserValidator>());
        
        // Act
        var token = await service.Authenticate(model);
        
        // Assert
        Assert.NotNull(token);
        Assert.NotEqual(string.Empty, token);
    }

    [Fact]
    public async Task AuthenticateCase_Should_Throw_ServiceException_If_Password_Is_Wrong()
    {
        // Arrange
        var password = "password";
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "somename",
            Email = "good@mail.com",
            Password = Provider.Get<IPasswordHasher>().GenerateHash(password),
            Phone = "+123456789",
            RoleId = 1,
            IsDeleted = false
        };
        var model = new AuthenticateUserModel
        {
            Email = user.Email,
            Password = "bad" + password
        };
        var repository = new Mock<IUserRepository>();
        repository.Setup(x=> x.GetByEmailAsync(It.IsAny<User>())).ReturnsAsync(user);
        var service = new UserService(Provider.Get<IMapper>(),
            repository.Object,
            Provider.Get<IJwtProvider>(),
            Provider.Get<IPasswordHasher>(),
            Provider.Get<UserValidator>());
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await service.Authenticate(model));
    }
}