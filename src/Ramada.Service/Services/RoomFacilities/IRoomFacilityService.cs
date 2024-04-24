using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Facilities;
using Ramada.Service.DTOs.RoomFacilities;

namespace Ramada.Service.Services.RoomFacilities;

public interface IRoomFacilityService
{
    ValueTask<RoomFacilityViewModel> CreateAsync(RoomFacilityCreateModel model);
    ValueTask<RoomFacilityViewModel> UpdateAsync(long id, RoomFacilityUpdateModel model);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<RoomFacilityViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<RoomFacilityViewModel>> GetAllAsync(PaginationParams @params, Filter filter);
}
