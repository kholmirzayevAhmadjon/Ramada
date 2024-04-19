using Ramada.Domain.Enums;

namespace Ramada.Service.DTOs.Rooms;

public class RoomUpdateModel
{
    public long HostelId { get; set; }
    public decimal Price { get; set; }
    public RoomStatus Status { get; set; }
    public int RoomNumber { get; set; }
    public string Descreption { get; set; }
    public int Floor { get; set; }
    public int Size { get; set; }
    public int MaxPeopleSize { get; set; }
}
