namespace WebApi.Models.Response;

public class AuthorizeResponse
{
    public Guid UserId { get; set; }
    
    public int RoleId { get; set; }
}