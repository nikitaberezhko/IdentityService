using Domain;

namespace Services.Auth.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(User user);

    User VerifyToken(string token);
}