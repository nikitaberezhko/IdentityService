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
    public async Task CreateModel_MustBeValid()
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
    public async Task CreateModel_MustThrowBecauseEmailIsInvalid()
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
    public async Task CreateModel_MustThrowBecauseRoleIdIsInvalid()
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
    public async Task CreateModel_MustThrowBecausePasswordIsEmpty()
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
    public async Task CreateModel_MustThrowBecauseNameIsEmpty()
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
    public async Task CreateModel_MustThrowBecausePhoneIsInvalid()
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
    public async Task CreateModel_MustThrowBecausePasswordLengthIsInvalid()
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
    public async Task CreateModel_MustThrowBecausePhoneIsEmpty()
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
        new(ProviderCreator.Get<IValidator<CreateUserModel>>(),
            new Mock<IValidator<DeleteUserModel>>().Object,
            new Mock<IValidator<AuthorizeUserModel>>().Object,
            new Mock<IValidator<AuthenticateUserModel>>().Object);
}