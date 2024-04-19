using Ramada.Service.DTOs.Facilities;
using Ramada.Service.DTOs.Rooms;

namespace Ramada.Service.DTOs.RoomFacilities;

public class RoomFacilityViewModel
{
    public long Id { get; set; }
    public RoomViewModel Room { get; set; }
    public FacilityViewModel Facility { get; set; }
    public int Count { get; set; }
}
