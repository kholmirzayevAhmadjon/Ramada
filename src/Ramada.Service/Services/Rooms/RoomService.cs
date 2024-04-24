using AutoMapper;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Rooms;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Rooms;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;
using Ramada.Service.Services.Hostels;
using Ramada.Service.Validators.Rooms;

namespace Ramada.Service.Services.Rooms;

public class RoomService(IUnitOfWork unitOfWork,
                         IMapper mapper,
                         IHostelService hostelService,
                         RoomCreateModelValidator roomCreateModelValidator,
                         RoomUpdateModelValidator roomUpdateModelValidator) : IRoomService
{
    public async ValueTask<RoomViewModel> CreateAsync(RoomCreateModel model)
    {
        await roomCreateModelValidator.ValidateOrPanicAsync(model);

        var hostel = await hostelService.GetByIdAsync(model.HostelId);
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

    public async ValueTask<IEnumerable<RoomViewModel>> GetAllAsync(PaginationParams @params,
                                                                   Filter filter,
                                                                   string search = null)
    {
        var rooms = unitOfWork.Rooms.SelectAsQueryable(includes: ["Hostel", "Facilities", "Assets", "Bookings"]).OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            rooms = rooms.Where(r => r.Description.ToLower().Contains(search.ToLower()));

        return await Task.FromResult(mapper.Map<IEnumerable<RoomViewModel>>(rooms.ToPaginate(@params)));
    }

    public async ValueTask<RoomViewModel> GetByIdAsync(long id)
    {
        var room = await unitOfWork.Rooms.SelectAsync(room => room.Id == id, ["Hostel"])
            ?? throw new NotFoundException($"Room is not found with this id: {id}");

        return mapper.Map<RoomViewModel>(room);
    }

    public async ValueTask<RoomViewModel> UpdateAsync(long id, RoomUpdateModel model)
    {
        await roomUpdateModelValidator.ValidateOrPanicAsync(model);

        var existRoom = await unitOfWork.Rooms.SelectAsync(room => room.Id == id)
            ?? throw new NotFoundException($"Room is not found with this id: {id}");
        var hostel = await hostelService.GetByIdAsync(model.HostelId);

        mapper.Map(model, existRoom);
        existRoom.UpdatedByUserId = HttpContextHelper.UserId;

        existRoom = await unitOfWork.Rooms.UpdateAsync(existRoom);
        await unitOfWork.SaveAsync();
        return mapper.Map<RoomViewModel>(existRoom);
    }
}
