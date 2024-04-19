using Ramada.Service.DTOs.Assets;
using Ramada.Service.DTOs.Bookings;
using Ramada.Service.DTOs.Users;

namespace Ramada.Service.DTOs.Customers;

public class CustomerViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AssetViewModel Asset { get; set; }
    public IEnumerable<BookingViewModel> Bookings { get; set; }
}
