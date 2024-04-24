using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Rooms;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.RoomFacilities;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;
using Ramada.Service.Services.Facilities;
using Ramada.Service.Services.Rooms;
using Ramada.Service.Validators.RoomFacilities;

namespace Ramada.Service.Services.RoomFacilities;

public class RoomFacilityService(IUnitOfWork unitOfWork,
                                 IRoomService roomService,
                                 IFacilityService facilityService,
                                 IMapper mapper,
                                 RoomFacilityCreateModelValidator roomFacilityCreateModelValidator,
                                 RoomFacilityUpdateModelValidator roomFacilityUpdateModelValidator) : IRoomFacilityService
{
    public async ValueTask<RoomFacilityViewModel> CreateAsync(RoomFacilityCreateModel model)
    {
        await roomFacilityCreateModelValidator.ValidateOrPanicAsync(model);

        var room = await roomService.GetByIdAsync(model.RoomId);
        var facility = await facilityService.GetByIdAsync(model.FacilityId);

        var roomFacility = mapper.Map<RoomFacility>(model);
        roomFacility.CreatedByUserId = HttpContextHelper.UserId;
        var createRoomFacility = await unitOfWork.RoomFacilities.InsertAsync(roomFacility);
        await unitOfWork.SaveAsync();

        var result = mapper.Map<RoomFacilityViewModel>(createRoomFacility);
        result.Room = room;
        result.Facility = facility;

        return result;
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
        var roomFacility = await unitOfWork.RoomFacilities.SelectAsync(r => r.Id == id, ["Room", "Facility"])
            ?? throw new NotFoundException($"RoomFacility is not found with this id: {id}");

        return mapper.Map<RoomFacilityViewModel>(roomFacility);
    }

    public async ValueTask<IEnumerable<RoomFacilityViewModel>> GetAllAsync(PaginationParams @params,
                                                                           Filter filter)
    {
        var roomFacilities = await unitOfWork.RoomFacilities
            .SelectAsQueryable(includes: ["Room", "Facility"])
            .OrderBy(filter)
            .ToPaginate(@params)
            .ToListAsync();

        return mapper.Map<IEnumerable<RoomFacilityViewModel>>(roomFacilities);
    }

    public async ValueTask<RoomFacilityViewModel> UpdateAsync(long id, RoomFacilityUpdateModel model)
    {
        await roomFacilityUpdateModelValidator.ValidateOrPanicAsync(model);

        var roomFacility = await unitOfWork.RoomFacilities.SelectAsync(r => r.Id == id)
            ?? throw new NotFoundException($"RoomFacility is not found with this id: {id}");
        var room = await roomService.GetByIdAsync(model.RoomId);
        var facility = await facilityService.GetByIdAsync(model.FacilityId);

        mapper.Map(model, roomFacility);
        roomFacility.UpdatedByUserId = HttpContextHelper.UserId;

        var updateRoomFacility = await unitOfWork.RoomFacilities.UpdateAsync(roomFacility);
        await unitOfWork.SaveAsync();

        var result = mapper.Map<RoomFacilityViewModel>(updateRoomFacility);
        result.Facility = facility;
        result.Room = room;

        return result;
    }
}
