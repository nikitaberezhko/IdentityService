namespace Exceptions.Services;

public class ServiceException : Exception
{
    public string Title { get; set; }
    
    public string Message { get; set; }
    
    public int StatusCode { get; set; }
}