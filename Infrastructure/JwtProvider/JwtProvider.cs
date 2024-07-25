using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Exceptions.Infrastructure;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Auth.Interfaces;

namespace Infrastructure.JwtProvider;

public class JwtProvider(IOptions<JwtOptions> jwtOptions) : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    
    public string GenerateToken(User user)
    {
        Claim[] claims = 
        [
            new("userId", user.Id.ToString()),
            new("roleId", user.RoleId.ToString())
        ];

        var signingCredential = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredential,
            issuer: _jwtOptions.Issuer,
            expires: DateTime.UtcNow.AddHours(_jwtOptions.Expiration)
        );
        
        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }
    
    public User VerifyToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
            }, out SecurityToken validatedToken);
            
            
            var jwtToken = (JwtSecurityToken)validatedToken;
            var result = new User
            {
                Id = Guid.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value),
                RoleId = int.Parse(jwtToken.Claims.First(x => x.Type == "roleId").Value)
            };
            
            return result;
        }
        catch
        {
            throw new InfrastructureException()
            {
                Title = "Unauthorized",
                Message = "Access token is invalid",
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }
    }
}