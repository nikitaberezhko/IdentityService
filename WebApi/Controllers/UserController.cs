using Asp.Versioning;
using AutoMapper;
using CommonModel.Contracts;
using IdentityService.Contracts.Request;
using IdentityService.Contracts.Response;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Request;
using Services.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/users")]
[ApiVersion(1.0)]
public class UserController(
    IUserService userService,
    IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CommonResponse<CreateUserResponse>>> Create(
        CreateUserRequest request)
    {
        var id = await userService.Create(mapper.Map<CreateUserModel>(request));

        var response = new CreatedResult(nameof(Create),
            new CommonResponse<CreateUserResponse>
            {
                Data = new CreateUserResponse { Id = id },
            });
        
        return response;
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<CommonResponse<DeleteUserResponse>>> Delete(
        [FromRoute] DeleteUserRequest request)
    {
        var user = await userService.Delete(mapper.Map<DeleteUserModel>(request));

        var response = new CommonResponse<DeleteUserResponse>
        {
            Data = new DeleteUserResponse
            {
                Id = user.Id,
                Name = user.Name,
                RoleId = user.RoleId,
                Phone = user.Phone
            }
        };
        
        return response;
    }
    
    [HttpPost("authenticate")]
    public async Task<ActionResult<CommonResponse<AuthenticateUserResponse>>> Authenticate(
        AuthenticateUserRequest request)
    {
        var token = await userService.Authenticate(
            mapper.Map<AuthenticateUserModel>(request));

        var response = new CommonResponse<AuthenticateUserResponse>
        {
            Data = new AuthenticateUserResponse { Token = token }
        };
        return response;
    }
    
    [HttpPost("authorize")]
    public async Task<ActionResult<CommonResponse<AuthorizeResponse>>> Authorize(
        AuthorizeUserRequest request)
    {
        var result = await userService.Authorize(
            mapper.Map<AuthorizeUserModel>(request));

        var authModel = mapper.Map<AuthorizeResponse>(result);
        var response = new CommonResponse<AuthorizeResponse>
        {
            Data = new AuthorizeResponse
            {
                UserId = authModel.UserId,
                RoleId = authModel.RoleId
            }
        };
        
        return response;
    }
}