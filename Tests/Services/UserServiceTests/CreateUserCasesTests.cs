using AutoMapper;
using Domain;
using Moq;
using Services.Auth.Interfaces;
using Services.Models.Request;
using Services.Repositories.Interfaces;
using Services.Services.Implementations;
using Services.Validation;
using Xunit;

namespace Tests.Services.UserServiceTests;

public class CreateUserCasesTests
{
    [Fact]
    public async Task CreateModel_MustReturnCreatedUserId()
    {
        // Arrange
        var service = CreateUserServiceForCreateCase();
        var model = new CreateUserModel
        {
            Name = "somename",
            Email = "good@mail.com",
            Password = "password",
            Phone = "+123456789",
            RoleId = 1
        };
        
        // Act
        var id = await service.Create(model);

        // Assert
        Assert.NotEqual(Guid.Empty, id);
    }
    
    private UserService CreateUserServiceForCreateCase()
    {
        var repository = new Mock<IUserRepository>();
        repository.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(() => Guid.NewGuid());
        
        return new UserService(Provider.Get<IMapper>(),
            repository.Object,
            Provider.Get<IJwtProvider>(),
            Provider.Get<IPasswordHasher>(),
            Provider.Get<UserValidator>());
    }
}