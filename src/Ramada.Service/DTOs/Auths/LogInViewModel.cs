using Ramada.Service.DTOs.Users;

namespace Ramada.Service.DTOs.Auths;

public class LogInViewModel
{
    public UserViewModel User { get; set; }
    public string Token { get; set; }
}
