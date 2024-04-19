namespace Ramada.Service.DTOs.RoomFacilities;

public class RoomFacilityCreateModel
{
    public long RoomId { get; set; }
    public long FacilityId { get; set; }
    public int Count { get; set; }
}
