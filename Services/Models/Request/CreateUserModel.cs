namespace Services.Models.Request;

public class CreateUserModel
{
    public int RoleId { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public string Name { get; set; }
    
    public string Phone { get; set; }
}