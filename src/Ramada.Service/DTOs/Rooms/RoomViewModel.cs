using Ramada.Domain.Enums;
using Ramada.Service.DTOs.Bookings;
using Ramada.Service.DTOs.Hostels;
using Ramada.Service.DTOs.RoomAssets;
using Ramada.Service.DTOs.RoomFacilities;

namespace Ramada.Service.DTOs.Rooms;

public class RoomViewModel
{
    public long Id { get; set; }
    public HostelViewModel Hostel { get; set; }
    public decimal Price { get; set; }
    public RoomStatus Status { get; set; }
    public int RoomNumber { get; set; }
    public string Description { get; set; }
    public int Floor { get; set; }
    public string Size { get; set; }
    public int MaxPeopleSize { get; set; }
    public IEnumerable<RoomFacilityViewModel> Facilities { get; set; }
    public IEnumerable<RoomAssetViewModel> Assets { get; set; }
    public IEnumerable<BookingViewModel> Bookings { get; set; }
}
