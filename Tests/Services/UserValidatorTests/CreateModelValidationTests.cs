using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.Services.UserValidatorTests;

public class CreateModelValidationTests
{
    [Fact]
    public async Task ValidateAsync_Should_Be_Valid_With_Valid_Model()
    {
        // Arrange
        var validator = CreateUserValidatorForCreateModel();
        var model = new CreateUserModel
        {
            RoleId = 1,
            Email = "good@mail.com",
            Password = "password",
            Name = "somename",
            Phone = "+123456789"
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
        var validator = CreateUserValidatorForCreateModel();
        var model = new CreateUserModel
        {
            RoleId = 1,
            Email = "bademail",
            Password = "password",
            Name = "somename",
            Phone = "+123456789"
        };

        // Act

        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsyncShould_Throw_ServiceException_If_RoleId_Is_Invalid()
    {
        // Arrange
        var validator = CreateUserValidatorForCreateModel();
        var model = new CreateUserModel
        {
            RoleId = 0,
            Email = "good@mail.com",
            Password = "password",
            Name = "somename",
            Phone = "+123456789"
        };

        // Act

        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Password_Is_Empty()
    {
        // Arrange
        var validator = CreateUserValidatorForCreateModel();
        var model = new CreateUserModel
        {
            RoleId = 1,
            Email = "good@mail.com",
            Password = "",
            Name = "somename",
            Phone = "+123456789"
        };

        // Act

        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Name_Is_Empty()
    {
        // Arrange
        var validator = CreateUserValidatorForCreateModel();
        var model = new CreateUserModel
        {
            RoleId = 1,
            Email = "good@mail.com",
            Password = "password",
            Name = "",
            Phone = "+123456789"
        };

        // Act

        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Phone_Is_Invalid()
    {
        // Arrange
        var validator = CreateUserValidatorForCreateModel();
        var model = new CreateUserModel
        {
            RoleId = 1,
            Email = "good@mail.com",
            Password = "password",
            Name = "somename",
            Phone = "+123456789m"
        };

        // Act

        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }
    
    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Password_Length_Less_Than_6()
    {
        // Arrange
        var validator = CreateUserValidatorForCreateModel();
        var model = new CreateUserModel
        {
            RoleId = 1,
            Email = "good@mail.com",
            Password = "pass",
            Name = "somename",
            Phone = "+123456789"
        };

        // Act

        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Phone_Is_Empty()
    {
        // Arrange
        var validator = CreateUserValidatorForCreateModel();
        var model = new CreateUserModel
        {
            RoleId = 1,
            Email = "good@mail.com",
            Password = "password",
            Name = "somename",
            Phone = ""
        };

        // Act

        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => await validator.ValidateAsync(model));
    }


    private UserValidator CreateUserValidatorForCreateModel() =>
        new(Provider.Get<IValidator<CreateUserModel>>(),
            new Mock<IValidator<DeleteUserModel>>().Object,
            new Mock<IValidator<AuthorizeUserModel>>().Object,
            new Mock<IValidator<AuthenticateUserModel>>().Object);
}