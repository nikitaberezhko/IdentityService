namespace Exceptions.Infrastructure;

public class InfrastructureException : Exception
{
    public string Title { get; set; }
    
    public string Message { get; set; }
    
    public int StatusCode { get; set; }
}