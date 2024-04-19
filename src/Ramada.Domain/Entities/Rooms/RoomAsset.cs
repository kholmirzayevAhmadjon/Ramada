using Ramada.Domain.Entities.Commons;

namespace Ramada.Domain.Entities.Rooms;

public class RoomAsset
{
    public long RoomId { get; set; }
    public Room Room { get; set; }
    public long AssetId { get; set; }
    public Asset Asset { get; set; }
}