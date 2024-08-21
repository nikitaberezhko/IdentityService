using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.Services.UserValidatorTests;

public class AuthenticateModelValidationTests
{
    [Fact]
    public async Task ValidateAsync_Should_Be_Valid_With_Valid_Model()
    {
        // Arrange
        var validator = CreateUserValidatorForAuthenticateModel();
        var model = new AuthenticateUserModel
        {
            Email = "good@mail",
            Password = "password"
        };
        
        // Act
        var result = await validator.ValidateAsync(model);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Email_Is_Invalid()
    {
        // Arrange
        var validator = CreateUserValidatorForAuthenticateModel();
        var model = new AuthenticateUserModel
        {
            Email = "bademail",
            Password = "password"
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Password_Is_Empty()
    {
        // Arrange
        var validator = CreateUserValidatorForAuthenticateModel();
        var model = new AuthenticateUserModel
        {
            Email = "good@mail",
            Password = ""
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }
    
    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Password_Length_Is_Invalid()
    {
        // Arrange
        var validator = CreateUserValidatorForAuthenticateModel();
        var model = new AuthenticateUserModel
        {
            Email = "good@mail",
            Password = "pass"
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }
    
    private UserValidator CreateUserValidatorForAuthenticateModel() =>
        new(new Mock<IValidator<CreateUserModel>>().Object,
            new Mock<IValidator<DeleteUserModel>>().Object,
            new Mock<IValidator<AuthorizeUserModel>>().Object,
            Provider.Get<IValidator<AuthenticateUserModel>>());
}