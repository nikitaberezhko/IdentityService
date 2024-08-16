using Domain;
using Exceptions.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Interfaces;

namespace Infrastructure.Repositories.Implementations;

public class UserRepository(DbContext context) : IUserRepository
{
    public async Task<Guid> AddAsync(User user)
    {
        try
        {
            user.Id = Guid.NewGuid();
            await context.Set<User>().AddAsync(user);
            await context.SaveChangesAsync();
        
            return user.Id;
        }
        catch
        {
            throw new InfrastructureException
            {
                Title = "Create user failed",
                Message = "User with this email already exists",
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
        
    }
    
    public async Task<User> GetByEmailAsync(User user)
    {
        var result = await context.Set<User>()
            .FirstOrDefaultAsync(x => x.Email == user.Email && !x.IsDeleted);
        if (result != null)
            return result;
        
        throw new InfrastructureException
        {
            Title = "User not found",
            Message = $"User with this login not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    public async Task<User> DeleteAsync(User user)
    {
        var result = await context.Set<User>().FirstOrDefaultAsync(x => x.Id == user.Id && !x.IsDeleted);
        if (result != null)
        {
            result.IsDeleted = true;
            await context.SaveChangesAsync();

            return result;
        }

        throw new InfrastructureException()
        {
            Title = "User not found",
            Message = $"User with this id not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }
}