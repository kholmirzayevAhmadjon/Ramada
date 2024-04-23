using Ramada.Domain.Commons;

namespace Ramada.Domain.Entities.Users;

public class User : Auditable
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public long RoleId { get; set; }
    public Role Role { get; set; }
    public IEnumerable<UserPermissionn> Permissions { get; set; }
}
