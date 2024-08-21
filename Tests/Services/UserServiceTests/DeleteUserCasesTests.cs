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

public class DeleteUserCasesTests
{
    [Fact]
    public async Task DeleteCase_Should_Return_Equal_Id()
    {
        // Arrange
        var id = Guid.NewGuid();
        var model = new DeleteUserModel { Id = id };
        var repository = new Mock<IUserRepository>();
        repository.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(() => new User
        {
            Id = id,
            Name = "somename",
            Email = "good@mail.com",
            Password = "password",
            Phone = "+123456789",
            RoleId = 1,
            IsDeleted = false
        });
        var service = new UserService(Provider.Get<IMapper>(),
            repository.Object,
            Provider.Get<IJwtProvider>(),
            Provider.Get<IPasswordHasher>(),
            Provider.Get<UserValidator>());
        
        // Act
        var result = await service.Delete(model);
        
        // Assert
        Assert.Equal(model.Id, result.Id);
    }
}