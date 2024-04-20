using Ramada.Service.DTOs.Assets;
using Ramada.Service.DTOs.Rooms;

namespace Ramada.Service.DTOs.RoomAssets;

public class RoomAssetViewModel
{
    public long Id { get; set; }
    public RoomViewModel Room { get; set; }
    public AssetViewModel Asset { get; set; }
}
