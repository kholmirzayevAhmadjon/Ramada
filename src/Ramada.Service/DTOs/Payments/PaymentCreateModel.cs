namespace Ramada.Service.DTOs.Payments;

public class PaymentCreateModel
{
    public long BookingId { get; set; }
    public decimal TotalPrice { get; set; }
}
