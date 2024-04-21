using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Hostels;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Hostels;
using Ramada.Service.Exceptions; 
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;
using Ramada.Service.Services.Assets;
using Ramada.Service.Services.Users;

namespace Ramada.Service.Services.Hostels;

public class HostelService(IUnitOfWork unitOfWork,
                           IUserService userService,
                           IAssetService assetService,
                           IMapper mapper) : IHostelService
{
    public async ValueTask<HostelViewModel> CreateAsync(HostelCreateModel hostel)
    {
        var user = await userService.GetByIdAsync(hostel.UserId);
        var asset = await assetService.GetByIdAsync(hostel.AssetId ?? 0);

        var mappedHostel = mapper.Map<Hostel>(hostel);
        mappedHostel.CreatedByUserId = HttpContextHelper.UserId;

        var createdHostel = await unitOfWork.Hostels.InsertAsync(mappedHostel);
        await unitOfWork.SaveAsync();

        var result = mapper.Map<HostelViewModel>(createdHostel);
        result.User = user;
        result.Asset = asset;

        return result;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var hostel = await unitOfWork.Hostels.SelectAsync(h => h.Id == id && !h.IsDeleted)
            ?? throw new NotFoundException($"Hostel is not found with this id: {id}");

        hostel.DeletedByUserId = HttpContextHelper.UserId;
        var deletedHostel = await unitOfWork.Hostels.DeleteAsync(hostel);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<HostelViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var hostels = unitOfWork.Hostels
            .SelectAsQueryable(h => !h.IsDeleted, ["User", "Asset", "Rooms"], false)
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            hostels = hostels.Where(h => h.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        var result = await hostels.ToPaginate(@params).ToListAsync();

        return mapper.Map<IEnumerable<HostelViewModel>>(result);
    }

    public async ValueTask<HostelViewModel> GetByIdAsync(long id)
    {
        var hostel = await unitOfWork.Hostels.SelectAsync(h => h.Id == id && !h.IsDeleted, ["User", "Asset", "Rooms"])
            ?? throw new NotFoundException($"Hostel is not found with this id: {id}");

        return mapper.Map<HostelViewModel>(hostel);
    }

    public async ValueTask<HostelViewModel> UpdateAsync(long id, HostelUpdateModel hostel)
    {
        var existHostel = await unitOfWork.Hostels.SelectAsync(h => h.Id == id && !h.IsDeleted)
            ?? throw new NotFoundException($"Hostel is not found with this id: {id}");
        var user = await userService.GetByIdAsync(hostel.UserId);
        var asset = await assetService.GetByIdAsync(hostel.AssetId ?? 0);

        mapper.Map(hostel, existHostel);
        existHostel.UpdatedByUserId = HttpContextHelper.UserId;

        var updatedHostel = await unitOfWork.Hostels.UpdateAsync(existHostel);
        await unitOfWork.SaveAsync();

        var result = mapper.Map<HostelViewModel>(updatedHostel);
        result.Asset = asset;
        result.User = user;

        return result;
    }
}