using AutoMapper;
using Domain;
using Services.Models.Request;
using Services.Models.Response;

namespace Services.Mapper;

public class ServiceMappingProfile : Profile
{
    
    public ServiceMappingProfile()
    {
        // Request models -> Domain models
        CreateMap<AuthenticateUserModel, User>()
            .ForMember(d => d.Email, map => map.MapFrom(c => c.Email))
            .ForMember(d => d.Password, map => map.MapFrom(c => c.Password))
            .ForMember(d => d.Id, map => map.Ignore())
            .ForMember(d => d.Role, map => map.Ignore())
            .ForMember(d => d.RoleId, map => map.Ignore())
            .ForMember(d => d.Name, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore())
            .ForMember(d => d.Phone, map => map.Ignore());  
        
        CreateMap<CreateUserModel, User>()
            .ForMember(d => d.RoleId, map => map.MapFrom(c => c.RoleId))
            .ForMember(d => d.Email, map => map.MapFrom(c => c.Email))
            .ForMember(d => d.Password, map => map.MapFrom(c => c.Password))
            .ForMember(d => d.Name, map => map.MapFrom(c => c.Name))
            .ForMember(d => d.Phone, map => map.MapFrom(c => c.Phone))
            .ForMember(d => d.Id, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore())
            .ForMember(d => d.Role, map => map.Ignore());   

        CreateMap<DeleteUserModel, User>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.RoleId, map => map.Ignore())
            .ForMember(d => d.Email, map => map.Ignore())
            .ForMember(d => d.Password, map => map.Ignore())
            .ForMember(d => d.Name, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore())
            .ForMember(d => d.Role, map => map.Ignore())
            .ForMember(d => d.Phone, map => map.Ignore());

        
        // Domain models -> Response models
        CreateMap<User, UserModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.RoleId, map => map.MapFrom(c => c.RoleId))
            .ForMember(d => d.Name, map => map.MapFrom(c => c.Name))
            .ForMember(d => d.Phone, map => map.MapFrom(c => c.Phone));
    }
}