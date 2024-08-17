using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.Services.UserValidatorTests;

public class AuthorizeModelValidationTests
{
    [Fact]
    public async Task AuthorizeModel_MustBeValid()
    {
        // Arrange
        var validator = CreateUserValidatorForAuthorizeModel();
        var model = new AuthorizeUserModel
        {
            Token = "tokentokentokentoken"
        };
        
        // Act
        var result = await validator.ValidateAsync(model);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task AuthorizeModel_MustThrowBecauseTokenIsEmpty()
    {
        // Arrange
        var validator = CreateUserValidatorForAuthorizeModel();
        var model = new AuthorizeUserModel
        {
            Token = ""
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }
    
    private UserValidator CreateUserValidatorForAuthorizeModel() =>
        new(new Mock<IValidator<CreateUserModel>>().Object,
            new Mock<IValidator<DeleteUserModel>>().Object,
            ProviderCreator.Get<IValidator<AuthorizeUserModel>>(),
            new Mock<IValidator<AuthenticateUserModel>>().Object);
}