using Ramada.Domain.Enums;

namespace Ramada.Service.DTOs.Rooms;

public class RoomCreateModel
{
    public long HostelId { get; set; }
    public decimal Price { get; set; }
    public RoomStatus Status { get; set; }
    public int RoomNumber { get; set; }
    public string Description { get; set; }
    public int Floor { get; set; }
    public string Size { get; set; }
    public int MaxPeopleSize { get; set; }
}