﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Hostels;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Hostels;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;
using Ramada.Service.Services.Addresses;
using Ramada.Service.Services.Users;
using Ramada.Service.Validators.Hostels;

namespace Ramada.Service.Services.Hostels;

public class HostelService(IUnitOfWork unitOfWork,
                           IUserService userService,
                           IAddressService addressService,
                           IMapper mapper,
                           HostelCreateModelValidator hostelCreateModelValidator,
                           HostelUpdateModelValidator hostelUpdateModelValidator) : IHostelService
{
    public async ValueTask<HostelViewModel> CreateAsync(HostelCreateModel hostel)
    {
        await hostelCreateModelValidator.ValidateOrPanicAsync(hostel);

        var user = await userService.GetByIdAsync(hostel.UserId);
        var address = await addressService.GetByIdAsync(hostel.AddressId);

        var mappedHostel = mapper.Map<Hostel>(hostel);
        mappedHostel.CreatedByUserId = HttpContextHelper.UserId;

        var createdHostel = await unitOfWork.Hostels.InsertAsync(mappedHostel);
        await unitOfWork.SaveAsync();

        var result = mapper.Map<HostelViewModel>(createdHostel);
        result.User = user;
        result.Address = address;

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
            .SelectAsQueryable(h => !h.IsDeleted, ["User", "Asset", "Rooms", "Address"], false)
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            hostels = hostels.Where(h => h.Name.ToLower().Contains(search.ToLower()));

        var result = await hostels.ToPaginate(@params).ToListAsync();

        return mapper.Map<IEnumerable<HostelViewModel>>(result);
    }

    public async ValueTask<HostelViewModel> GetByIdAsync(long id)
    {
        var hostel = await unitOfWork.Hostels.SelectAsync(h => h.Id == id && !h.IsDeleted, ["User", "Asset", "Rooms", "Address"])
            ?? throw new NotFoundException($"Hostel is not found with this id: {id}");

        return mapper.Map<HostelViewModel>(hostel);
    }

    public async ValueTask<HostelViewModel> UpdateAsync(long id, HostelUpdateModel hostel)
    {
        await hostelUpdateModelValidator.ValidateOrPanicAsync(hostel);

        var existHostel = await unitOfWork.Hostels.SelectAsync(h => h.Id == id && !h.IsDeleted)
            ?? throw new NotFoundException($"Hostel is not found with this id: {id}");
        var user = await userService.GetByIdAsync(hostel.UserId);
        var address = await addressService.GetByIdAsync(hostel.AddressId);

        mapper.Map(hostel, existHostel);
        existHostel.UpdatedByUserId = HttpContextHelper.UserId;

        var updatedHostel = await unitOfWork.Hostels.UpdateAsync(existHostel);
        await unitOfWork.SaveAsync();

        var result = mapper.Map<HostelViewModel>(updatedHostel);
        result.Address = address;
        result.User = user;

        return result;
    }
}