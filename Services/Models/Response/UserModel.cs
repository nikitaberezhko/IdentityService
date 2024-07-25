namespace Services.Models.Response;

public class UserModel
{
    public Guid Id { get; set; }
    
    public int RoleId { get; set; }
    
    public string Name { get; set; }
    
    public string Phone { get; set; }
}