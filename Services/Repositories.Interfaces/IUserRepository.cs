using Domain;

namespace Services.Repositories.Interfaces;

public interface IUserRepository
{
    Task<Guid> AddAsync(User user);

    Task<User> GetByEmailAsync(User user);

    Task<User> DeleteAsync(User user);
}