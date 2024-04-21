using Ramada.Domain.Commons;
using Ramada.Domain.Entities.Customers;
using Ramada.Domain.Entities.Payments;
using Ramada.Domain.Entities.Rooms;
using Ramada.Domain.Enums;

namespace Ramada.Domain.Entities.Bookings;

public class Booking : Auditable
{
    public long CustomerId { get; set; }
    public Customer Customer { get; set; }
    public long RoomId { get; set; }
    public Room Room { get; set; }
    public int NumberOfPeople { get; set; }
    public DateTime StartDate { get; set; }
    public int NumberOfDays { get; set; }
    public BookingStatus Status { get; set; }
    public Payment Payment { get; set; }
}
