using Ramada.Service.Configurations;
using Ramada.Service.DTOs.RoomAssets;

namespace Ramada.Service.Services.RoomAssets;

public interface IRoomAssetService
{
    ValueTask<RoomAssetViewModel> CreateAsync(RoomAssetCreateModel model);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<RoomAssetViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<RoomAssetViewModel>> GetAllAsync(PaginationParams @params,
                                                            Filter filter,
                                                            string search = null);

}
