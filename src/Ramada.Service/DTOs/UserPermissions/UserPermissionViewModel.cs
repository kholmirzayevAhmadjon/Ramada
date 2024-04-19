using Ramada.Domain.Entities.Users;
using Ramada.Service.DTOs.Users;

namespace Ramada.Service.DTOs.UserPermissions;

public class UserPermissionViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public PermissionViewModel Permission { get; set; }
}
