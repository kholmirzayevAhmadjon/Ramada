namespace Ramada.Service.DTOs.Addresses;

public class AddressCreateModel
{
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int PostCode { get; set; }
    public int HouseNumber { get; set; }
}
