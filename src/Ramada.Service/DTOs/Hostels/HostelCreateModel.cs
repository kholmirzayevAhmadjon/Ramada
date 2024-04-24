namespace Ramada.Service.DTOs.Hostels;

public class HostelCreateModel
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long AddressId { get; set; }
}
