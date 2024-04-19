namespace Ramada.Service.DTOs.Users;

public class UserCreateModel
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public long RoleId { get; set; }
}
