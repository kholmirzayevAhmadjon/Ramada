using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Rooms;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.RoomAssets;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;
using Ramada.Service.Services.Assets;
using Ramada.Service.Services.Rooms;

namespace Ramada.Service.Services.RoomAssets;

public class RoomAssetService(IMapper mapper,
                              IUnitOfWork unitOfWork,
                              IRoomService roomService,
                              IAssetService assetService) : IRoomAssetService
{
    public async ValueTask<RoomAssetViewModel> CreateAsync(RoomAssetCreateModel model)
    {
        var room = await roomService.GetByIdAsync(model.RoomId);
        var asset = await assetService.GetByIdAsync(model.AssetId);

        var roomAsset = mapper.Map<RoomAsset>(model);
        roomAsset.CreatedByUserId = HttpContextHelper.UserId;
        var createRoomAsset = await unitOfWork.RoomAssets.InsertAsync(roomAsset);
        await unitOfWork.SaveAsync();

        var result = mapper.Map<RoomAssetViewModel>(createRoomAsset);
        result.Room = room;
        result.Asset = asset;

        return result;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existRoomAsset = await unitOfWork.RoomAssets.SelectAsync(roomAsset => roomAsset.Id == id)
            ?? throw new NotFoundException($"RoomAsset is not found with this id: {id}");

        await unitOfWork.RoomAssets.DropAsync(existRoomAsset);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<RoomAssetViewModel>> GetAllAsync(PaginationParams @params,
                                                                        Filter filter,
                                                                        string search = null)
    {
        var roomAssets = await unitOfWork.RoomAssets
            .SelectAsQueryable(includes: ["Room", "Asset"])
            .OrderBy(filter)
            .ToPaginate(@params)
            .ToListAsync();

        return mapper.Map<IEnumerable<RoomAssetViewModel>>(roomAssets);
    }

    public async ValueTask<RoomAssetViewModel> GetByIdAsync(long id)
    {
        var existRoomAsset = await unitOfWork.RoomAssets.SelectAsync(roomAsset => roomAsset.Id == id, includes: ["Room", "Asset"])
            ?? throw new NotFoundException($"RoomAsset is not found with this id: {id}");

        return mapper.Map<RoomAssetViewModel>(existRoomAsset);
    }
}