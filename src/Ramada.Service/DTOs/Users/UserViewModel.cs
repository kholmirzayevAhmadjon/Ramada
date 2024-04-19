using Ramada.Service.DTOs.Roles;
using Ramada.Service.DTOs.UserPermissions;

namespace Ramada.Service.DTOs.Users;

public class UserViewModel
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public RoleViewModel Role { get; set; }
    public IEnumerable<UserPermissionViewModel> Permissions { get; set; }
}
