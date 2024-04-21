using Ramada.Domain.Commons;
using Ramada.Domain.Entities.Bookings;
using Ramada.Domain.Entities.Hostels;
using Ramada.Domain.Enums;

namespace Ramada.Domain.Entities.Rooms;

public class Room : Auditable
{
    public long HostelId { get; set; }
    public Hostel Hostel { get; set; }
    public decimal Price { get; set; }
    public RoomStatus Status { get; set; }
    public int RoomNumber { get; set; }
    public string Description { get; set; }
    public int Floor { get; set; }
    public string Size { get; set; }
    public int MaxPeopleSize { get; set; }
    public IEnumerable<RoomFacility> Facilities { get; set; }
    public IEnumerable<RoomAsset> Assets { get; set; }
    public IEnumerable<Booking> Bookings { get; set; }
}
