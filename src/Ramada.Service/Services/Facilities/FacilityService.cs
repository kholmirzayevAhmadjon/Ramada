using AutoMapper;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Rooms;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Facilities;
using Ramada.Service.DTOs.Roles;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;

namespace Ramada.Service.Services.Facilities;

public class FacilityService(IUnitOfWork unitOfWork, IMapper mapper) : IFacilityService
{
    public async ValueTask<FacilityViewModel> Create(FacilityCreateModel model)
    {
        var facility = await unitOfWork.Facilities.SelectAsync(expression: facility => facility.Name.ToLower() == model.Name.ToLower())
            ?? throw new AlreadyExistException("This facility already exists");

        var result = mapper.Map<Facility>(facility);
        facility.CreatedByUserId = HttpContextHelper.UserId;
        var createFacility = await unitOfWork.Facilities.InsertAsync(result);
        await unitOfWork.SaveAsync();
        return mapper.Map<FacilityViewModel>(createFacility);
    }

    public async ValueTask<bool> Delete(long id)
    {
        var facility = await unitOfWork.Facilities.SelectAsync(expression: facility => facility.Id == id)
            ?? throw new NotFoundException($"This facility not found ID = {id}");

        var deleteFacility = await unitOfWork.Facilities.DropAsync(facility);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<IEnumerable<FacilityViewModel>> GetAll(PaginationParams @params, Filter filter, string search = null)
    {
        var facility = unitOfWork.Facilities.SelectAsQueryable().OrderBy(filter);
        if (!string.IsNullOrEmpty(search))
            facility = facility.Where(f => f.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await Task.FromResult(mapper.Map<IEnumerable<FacilityViewModel>>(facility.ToPaginate(@params)));
    }

    public async ValueTask<FacilityViewModel> GetById(long id)
    {
        var facility = await unitOfWork.Facilities.SelectAsync(facility => facility.Id == id)
            ?? throw new NotFoundException($"This facility not found ID = {id}");

        return mapper.Map<FacilityViewModel>(facility);
    }

    public async ValueTask<FacilityViewModel> Update(long id, FacilityUpdateModel model)
    {
        var existFacility = await unitOfWork.Facilities.SelectAsync(facility => facility.Id == id)
            ?? throw new NotFoundException($"This facility not found ID = {id}");

        existFacility.Name = model.Name;
        existFacility.Description = model.Description;
        existFacility.UpdatedAt = DateTime.UtcNow;
        existFacility.CreatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Facilities.UpdateAsync(existFacility);
        await unitOfWork.SaveAsync();
        return mapper.Map<FacilityViewModel>(existFacility);
    }
}
