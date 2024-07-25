namespace Services.Models.Request;

public class AuthenticateUserModel
{
    public string Email { get; set; }
    
    public string Password { get; set; }
}