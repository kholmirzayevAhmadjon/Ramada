using Ramada.Domain.Commons;

namespace Ramada.Domain.Entities.Hostels;

public class Address : Auditable
{
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int PostCode { get; set; }
    public int HouseNumber { get; set; }
}