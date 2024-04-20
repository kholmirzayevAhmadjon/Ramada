using Ramada.Domain.Commons;

namespace Ramada.Domain.Entities.Rooms;

public class RoomFacility : Auditable
{
    public long RoomId { get; set; }
    public Room Room { get; set; }
    public long FacilityId { get; set; }
    public Facility Facility { get; set; }
    public int Count { get; set; }
}