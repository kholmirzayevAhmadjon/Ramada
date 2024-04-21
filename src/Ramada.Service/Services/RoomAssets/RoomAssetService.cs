using AutoMapper;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Rooms;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.RoomAssets;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;

namespace Ramada.Service.Services.RoomAssets;

public class RoomAssetService(IMapper mapper, IUnitOfWork unitOfWork) : IRoomAssetService
{
    public async ValueTask<RoomAssetViewModel> CreateAsync(RoomAssetCreateModel model)
    {
        var room = await unitOfWork.Rooms.SelectAsync(room => room.Id == model.RoomId)
            ?? throw new NotFoundException($"Room is not found with this id: {model.RoomId}");

        var asset = await unitOfWork.Assets.SelectAsync(asset => asset.Id == model.AssetId)
            ?? throw new NotFoundException($"Asset is not found with this id: {model.AssetId}");

        var roomAsset = mapper.Map<RoomAsset>(model);
        roomAsset.CreatedByUserId = HttpContextHelper.UserId;
        var createRoomAsset = await unitOfWork.RoomAssets.InsertAsync(roomAsset);
        await unitOfWork.SaveAsync();
        return mapper.Map<RoomAssetViewModel>(createRoomAsset);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existRoomAsset = await unitOfWork.RoomAssets.SelectAsync(roomAsset => roomAsset.Id == id)
            ?? throw new NotFoundException($"RoomAsset is not found with this id: {id}");
        await unitOfWork.RoomAssets.DropAsync(existRoomAsset);
        await unitOfWork.SaveAsync();
        return true;

    }

    public async ValueTask<IEnumerable<RoomAssetViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var roomAssets = unitOfWork.RoomAssets.SelectAsQueryable().OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            roomAssets = roomAssets.Where(f => Convert.ToString(f.RoomId).Contains(search, StringComparison.OrdinalIgnoreCase));

        return await Task.FromResult(mapper.Map<IEnumerable<RoomAssetViewModel>>(roomAssets.ToPaginate(@params)));
    }

    public async ValueTask<RoomAssetViewModel> GetByIdAsync(long id)
    {
        var existRoomAsset = await unitOfWork.RoomAssets.SelectAsync(roomAsset => roomAsset.Id == id)
            ?? throw new NotFoundException($"RoomAsset is not found with this id: {id}");

        return mapper.Map<RoomAssetViewModel>(existRoomAsset);
    }
}