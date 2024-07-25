using Exceptions.Infrastructure;
using Exceptions.Services;
using WebApi.Models;

namespace WebApi.Middlewares;

public class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger) 
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (InfrastructureException e)
        {
            logger.LogWarning(e, e.Message);

            await InterceptResponseAsync(
                context,
                e.Title,
                e.Message,
                e.StatusCode);
        }
        catch (ServiceException e)
        {
            logger.LogWarning(e, e.Message);
            
            await InterceptResponseAsync(
                context,
                e.Title,
                e.Message,
                e.StatusCode);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            
            await InterceptResponseAsync(context,
                "Unknown server error", 
                "Please retry query", 
                StatusCodes.Status500InternalServerError);
        }
    }
    
    private async Task InterceptResponseAsync(HttpContext context,
        string title, 
        string message, 
        int statusCode)
    {
        var response = new CommonResponse<Empty>
        {
            Data = null,
            Error = new Error
            {
                Title = title,
                Message = message,
                StatusCode = statusCode
            }
        };
            
        context.Response.Clear();
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}

record Empty;