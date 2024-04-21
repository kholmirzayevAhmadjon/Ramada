using Ramada.Domain.Entities.Rooms;
using Ramada.Domain.Enums;
using Ramada.Service.DTOs.Customers;
using Ramada.Service.DTOs.Payments;
using Ramada.Service.DTOs.Rooms;

namespace Ramada.Service.DTOs.Bookings;

public class BookingViewModel
{
    public long Id { get; set; }
    public CustomerViewModel Customer { get; set; }
    public RoomViewModel Room { get; set; }
    public int NumberOfPeople { get; set; }
    public DateTime StartDate { get; set; }
    public int NumberOfDays { get; set; }
    public BookingStatus Status { get; set; }
    public PaymentViewModel Payment { get; set; }
}
