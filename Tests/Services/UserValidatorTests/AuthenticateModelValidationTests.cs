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
    public async Task AuthenticateModel_MustBeValid()
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
    public async Task AuthenticateModel_MustThrowBecauseEmailIsInvalid()
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
    public async Task AuthenticateModel_MustThrowBecausePasswordIsEmpty()
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
    public async Task AuthenticateModel_MustThrowBecausePasswordLengthIsInvalid()
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
            ProviderCreator.Get<IValidator<AuthenticateUserModel>>());
}