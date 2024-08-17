using Services.Auth.Interfaces;
using Xunit;

namespace Tests.Infrastructure;

public class PasswordHasherTests
{
    [Fact]
    public async Task CreateHashAndVerify_MustReturnTrue()
    {
        // Arrange
        var passwordHasher = Provider.Get<IPasswordHasher>();
        var password = "password";
        
        // Act
        var hash = passwordHasher.GenerateHash(password);
        var result = passwordHasher.VerifyHash(password, hash);
        
        // Assert
        Assert.True(result);
    }
}