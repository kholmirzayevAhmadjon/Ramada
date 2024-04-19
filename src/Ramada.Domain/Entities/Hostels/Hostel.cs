using Ramada.Domain.Commons;
using Ramada.Domain.Entities.Commons;
using Ramada.Domain.Entities.Rooms;
using Ramada.Domain.Entities.Users;

namespace Ramada.Domain.Entities.Hostels;

public class Hostel : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long? AssetId { get; set; }
    public Asset Asset { get; set; }
    public long AdressId { get; set; }
    public Address Address { get; set; }
    public IEnumerable<Room> Rooms { get; set; }
}
