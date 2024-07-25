using Services.Models.Request;
using Services.Models.Response;

namespace Services.Services.Interfaces;

public interface IUserService
{
    public Task<Guid> Create(CreateUserModel model);

    public Task<string> Authenticate(AuthenticateUserModel model);

    public Task<UserModel> Authorize(AuthorizeUserModel model);

    public Task<UserModel> Delete(DeleteUserModel model);
}