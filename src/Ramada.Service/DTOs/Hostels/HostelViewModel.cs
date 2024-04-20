using Ramada.Service.DTOs.Addresses;
using Ramada.Service.DTOs.Assets;
using Ramada.Service.DTOs.Rooms;
using Ramada.Service.DTOs.Users;

namespace Ramada.Service.DTOs.Hostels;

public class HostelViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public AssetViewModel Asset { get; set; }
    public AddressViewModel Address { get; set; }
    public IEnumerable<RoomViewModel> Rooms { get; set; }
}
