using Ramada.Domain.Commons;
using Ramada.Domain.Entities.Bookings;

namespace Ramada.Domain.Entities.Payments;

public class Payment : Auditable
{
    public long BookingId { get; set; }
    public Booking Booking { get; set; }
    public decimal TotalPrice { get; set; }
}