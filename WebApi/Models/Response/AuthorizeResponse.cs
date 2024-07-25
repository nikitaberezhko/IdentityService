namespace Identity.Contracts.Responses;

public class AuthorizeResponse
{
    public Guid UserId { get; set; }
    
    public int RoleId { get; set; }
}