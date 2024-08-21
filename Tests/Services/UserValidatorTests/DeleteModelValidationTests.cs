using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.Services.UserValidatorTests;

public class DeleteModelValidationTests
{
    [Fact]
    public async Task ValidateAsync_Should_Be_Valid_With_Valid_Model()
    {
        // Arrange
        var validator = CreateUserValidatorForDeleteModel();
        var model = new DeleteUserModel
        {
            Id = Guid.NewGuid()
        };
        
        // Act
        var result = await validator.ValidateAsync(model);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Id_Is_Empty()
    {
        // Arrange
        var validator = CreateUserValidatorForDeleteModel();
        var model = new DeleteUserModel
        {
            Id = Guid.Empty
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Id_Is_Invalid()
    {
        // Arrange
        var validator = CreateUserValidatorForDeleteModel();
        var model = new DeleteUserModel
        {
            Id = Guid.Empty
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }
    
    private UserValidator CreateUserValidatorForDeleteModel() =>
        new(new Mock<IValidator<CreateUserModel>>().Object,
            Provider.Get<IValidator<DeleteUserModel>>(),
            new Mock<IValidator<AuthorizeUserModel>>().Object,
            new Mock<IValidator<AuthenticateUserModel>>().Object);
}