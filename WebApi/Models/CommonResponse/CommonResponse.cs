namespace WebApi.Models;

public class CommonResponse<T>
{
    public T? Data { get; set; }

    public Error? Error { get; set; } = null;
}