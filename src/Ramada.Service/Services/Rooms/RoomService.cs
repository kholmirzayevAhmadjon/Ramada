using AutoMapper;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Rooms;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Rooms;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;

namespace Ramada.Service.Services.Rooms;

public class RoomService(IUnitOfWork unitOfWork, IMapper mapper) : IRoomService
{
    public async ValueTask<RoomViewModel> CreateAsync(RoomCreateModel model)
    {
        var existRoom = await unitOfWork.Rooms.InsertAsync(mapper.Map<Room>(model));
        await unitOfWork.SaveAsync();
        return mapper.Map<RoomViewModel>(existRoom);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existRoom = await unitOfWork.Rooms.SelectAsync(room => room.Id == id)
            ?? throw new NotFoundException($"Room is not found with this id: {id}");

        await unitOfWork.Rooms.DropAsync(existRoom);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<IEnumerable<RoomViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var rooms = unitOfWork.Rooms.SelectAsQueryable().OrderBy(filter);
        if (!string.IsNullOrEmpty(search))
            rooms = rooms.Where(f => f.Size.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await Task.FromResult(mapper.Map<IEnumerable<RoomViewModel>>(rooms.ToPaginate(@params)));
    }

    public async ValueTask<RoomViewModel> GetByIdAsync(long id)
    {
        var room = await unitOfWork.Rooms.SelectAsync(room => room.Id == id)
            ?? throw new NotFoundException($"Room is not found with this id: {id}");

        return mapper.Map<RoomViewModel>(room);
    }

    public async ValueTask<RoomViewModel> UpdateAsync(long id, RoomUpdateModel model)
    {
        var existRoom = await unitOfWork.Rooms.SelectAsync(room => room.Id == id)
            ?? throw new NotFoundException($"Room is not found with this id: {id}");

        existRoom.Status = model.Status;
        existRoom.Size = model.Size;
        existRoom.Price = model.Price;
        existRoom.HostelId = model.HostelId;
        existRoom.RoomNumber = model.RoomNumber;
        existRoom.Floor = model.Floor;
        existRoom.Description = model.Description;
        existRoom.MaxPeopleSize = model.MaxPeopleSize;
        existRoom.UpdatedAt = DateTime.UtcNow;
        existRoom.UpdatedByUserId = HttpContextHelper.UserId;

        await unitOfWork.Rooms.UpdateAsync(existRoom);
        await unitOfWork.SaveAsync();
        return mapper.Map<RoomViewModel>(existRoom);
    }
}
