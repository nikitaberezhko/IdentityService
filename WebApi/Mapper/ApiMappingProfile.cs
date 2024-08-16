using AutoMapper;
using IdentityService.Contracts.Request;
using IdentityService.Contracts.Response;
using Services.Models.Request;
using Services.Models.Response;

namespace WebApi.Mapper;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        // Request -> Request models
        CreateMap<AuthenticateUserRequest, AuthenticateUserModel>()
            .ForMember(d => d.Email, map => map.MapFrom(c => c.Email))
            .ForMember(d => d.Password, map => map.MapFrom(c => c.Password));

        CreateMap<AuthorizeUserRequest, AuthorizeUserModel>()
            .ForMember(d => d.Token, map => map.MapFrom(c => c.Token));
        
        CreateMap<CreateUserRequest, CreateUserModel>()
            .ForMember(d => d.RoleId, map => map.MapFrom(c => c.RoleId))
            .ForMember(d => d.Email, map => map.MapFrom(c => c.Email))
            .ForMember(d => d.Password, map => map.MapFrom(c => c.Password))
            .ForMember(d => d.Name, map => map.MapFrom(c => c.Name))
            .ForMember(d => d.Phone, map => map.MapFrom(c => c.Phone));

        CreateMap<DeleteUserRequest, DeleteUserModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id));
        
        // Response models -> Response
        CreateMap<UserModel, AuthorizeResponse>()
            .ForMember(d => d.UserId, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.RoleId, map => map.MapFrom(c => c.RoleId));
        
        CreateMap<UserModel, DeleteUserResponse>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.RoleId, map => map.MapFrom(c => c.RoleId))
            .ForMember(d => d.Name, map => map.MapFrom(c => c.Name))
            .ForMember(d => d.Phone, map => map.MapFrom(c => c.Phone));
    }
}