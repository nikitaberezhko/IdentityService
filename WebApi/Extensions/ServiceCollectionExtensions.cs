using Asp.Versioning;
using FluentValidation;
using Infrastructure.JwtProvider;
using Infrastructure.PasswordHasher;
using Infrastructure.Repositories.Implementations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework;
using Services.Auth.Interfaces;
using Services.Mapper;
using Services.Models.Request;
using Services.Repositories.Interfaces;
using Services.Services.Implementations;
using Services.Services.Interfaces;
using Services.Validation;
using WebApi.Mapper;

namespace WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<DbContext, DataContext>();

        return services;
    }
    
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
    
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<AuthenticateUserModel>, AuthenticateUserValidator>();
        services.AddScoped<IValidator<AuthorizeUserModel>, AuthorizeUserValidator>();
        services.AddScoped<IValidator<CreateUserModel>, CreateUserValidator>();
        services.AddScoped<IValidator<DeleteUserModel>, DeleteUserValidator>();

        return services;
    }
    
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(ServiceMappingProfile),
            typeof(ApiMappingProfile));
        
        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }

    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
    
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlerMiddleware>();
        
        return services;
    }
    
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}