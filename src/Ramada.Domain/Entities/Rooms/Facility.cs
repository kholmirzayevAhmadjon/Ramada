using Ramada.Domain.Commons;

namespace Ramada.Domain.Entities.Rooms;

public class Facility : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
}