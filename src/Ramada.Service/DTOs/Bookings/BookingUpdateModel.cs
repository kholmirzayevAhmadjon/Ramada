using Ramada.Domain.Entities.Rooms;
using Ramada.Domain.Enums;

namespace Ramada.Service.DTOs.Bookings;

public class BookingUpdateModel
{
    public long Id { get; set; }
    public long RoomId { get; set; }
    public int NumberOfPeople { get; set; }
    public DateTime StartDate { get; set; }
    public int NumberOfDays { get; set; }
    public BookingStatus Status { get; set; }
}
