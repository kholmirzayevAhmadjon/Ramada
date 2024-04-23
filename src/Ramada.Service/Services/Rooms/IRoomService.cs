using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Rooms;

namespace Ramada.Service.Services.Rooms;

public interface IRoomService
{
    ValueTask<RoomViewModel> CreateAsync(RoomCreateModel model);
    ValueTask<RoomViewModel> UpdateAsync(long id, RoomUpdateModel model);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<RoomViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<RoomViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
