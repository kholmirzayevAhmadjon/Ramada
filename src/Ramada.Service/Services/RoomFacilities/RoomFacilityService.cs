using AutoMapper;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Rooms;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.RoomFacilities;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;

namespace Ramada.Service.Services.RoomFacilities;

public class RoomFacilityService(IUnitOfWork unitOfWork, IMapper mapper) : IRoomFacilityService
{
    public async ValueTask<RoomFacilityViewModel> CreateAsync(RoomFacilityCreateModel model)
    {
        var room = await unitOfWork.Rooms.SelectAsync(room => room.Id == model.RoomId)
            ?? throw new NotFoundException($"Room is not found with this id: {model.RoomId}");

        var facility = await unitOfWork.Facilities.SelectAsync(facility => facility.Id == model.FacilityId)
            ?? throw new NotFoundException($"Facility is not found with this id: {model.FacilityId}");

        var roomFacility = mapper.Map<RoomFacility>(model);
        roomFacility.CreatedByUserId = HttpContextHelper.UserId;
        var createRoomFacility = await unitOfWork.RoomFacilities.InsertAsync(roomFacility);
        await unitOfWork.SaveAsync();
        return mapper.Map<RoomFacilityViewModel>(createRoomFacility);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var roomFacility = await unitOfWork.RoomFacilities.SelectAsync(r => r.Id == id)
             ?? throw new NotFoundException($"RoomFacility is not found with this id: {id}");

        await unitOfWork.RoomFacilities.DropAsync(roomFacility);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<RoomFacilityViewModel> GetByIdAsync(long id)
    {
        var roomFacility = await unitOfWork.RoomFacilities.SelectAsync(r => r.Id == id)
            ?? throw new NotFoundException($"RoomFacility is not found with this id: {id}");

        return mapper.Map<RoomFacilityViewModel>(roomFacility);
    }

    public async ValueTask<IEnumerable<RoomFacilityViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var roomFacilities = unitOfWork.RoomFacilities.SelectAsQueryable().OrderBy(filter);

        if(!string.IsNullOrEmpty(search))
            roomFacilities = roomFacilities.Where(f => Convert.ToString(f.RoomId).Contains(search, StringComparison.OrdinalIgnoreCase));
       
        return await Task.FromResult(mapper.Map<IEnumerable<RoomFacilityViewModel>>(roomFacilities.ToPaginate(@params)));
    }

    public async ValueTask<RoomFacilityViewModel> UpdateAsync(long id, RoomFacilityUpdateModel model)
    {
        var roomFacility = await unitOfWork.RoomFacilities.SelectAsync(r => r.Id == id)
            ?? throw new NotFoundException($"RoomFacility is not found with this id: {id}");

        roomFacility.RoomId = model.RoomId;
        roomFacility.FacilityId = model.FacilityId;
        roomFacility.UpdatedByUserId = HttpContextHelper.UserId;
        roomFacility.UpdatedAt = DateTime.UtcNow;
        roomFacility.Count = model.Count;

        var updateRoomFacility = await unitOfWork.RoomFacilities.UpdateAsync(roomFacility);
        await unitOfWork.SaveAsync();

        return mapper.Map<RoomFacilityViewModel>(updateRoomFacility);
    }
}
