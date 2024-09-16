using Infrastructure.Settings;
using WebApi.Extensions;
using WebApi.Middlewares;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        builder.AddSerilog();

        services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
        
        services.AddControllers();
        
        // Extensions
        services.AddDataContext(builder.Configuration.GetConnectionString("DefaultConnectionString")!);
        services.AddSwagger();
        services.AddValidation();
        services.AddVersioning();
        services.AddMappers();
        services.AddServices();
        services.AddAuthServices();
        services.AddRepositories();
        services.AddExceptionHandling();
        services.AddTelemetry();

        var app = builder.Build();

        app.UseMiddleware<ExceptionHandlerMiddleware>();
        
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseHttpsRedirection();

        app.MapPrometheusScrapingEndpoint();

        app.MapControllers();
        app.Run();
    }
}