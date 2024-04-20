using Ramada.Service.DTOs.Bookings;

namespace Ramada.Service.DTOs.Payments;

public class PaymentViewModel
{
    public long Id { get; set; }
    public BookingViewModel Booking { get; set; }
    public decimal TotalPrice { get; set; }
}
