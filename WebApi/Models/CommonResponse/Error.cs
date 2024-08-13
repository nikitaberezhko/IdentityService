namespace WebApi.Models.CommonResponse;

public class Error
{
    public string Title { get; set; }

    public string Message { get; set; }
    
    public int StatusCode { get; set; }
}