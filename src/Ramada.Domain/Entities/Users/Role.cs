using Ramada.Domain.Commons;

namespace Ramada.Domain.Entities.Users;

public class Role : Auditable
{
    public string Name { get; set; }
}
