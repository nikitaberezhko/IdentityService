using Services.Auth.Interfaces;

namespace Infrastructure.PasswordHasher;

public class PasswordHasher : IPasswordHasher
{
    public string GenerateHash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }
    
    public bool VerifyHash(string password, string hash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
    }
}