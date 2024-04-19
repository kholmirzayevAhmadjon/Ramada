using Ramada.Domain.Commons;
using Ramada.Domain.Entities.Bookings;
using Ramada.Domain.Entities.Commons;
using Ramada.Domain.Entities.Users;

namespace Ramada.Domain.Entities.Customers;

public class Customer : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long? AssetId { get; set; }
    public Asset Asset { get; set; }
    public IEnumerable<Booking> Bookings { get; set; }
}
