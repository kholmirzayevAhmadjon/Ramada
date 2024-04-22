
using AutoMapper;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Hostels;
using Ramada.Domain.Entities.Rooms;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Addresses;
using Ramada.Service.DTOs.Rooms;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;
namespace Ramada.Service.Services.Addresses;

public class AddressService(IMapper mapper, IUnitOfWork unitOfWork) : IAddressService
{
    public async ValueTask<AddressViewModel> CreateAsync(AddressCreateModel addressCreateModel)
    {
        var existAddress = await unitOfWork.Addresses.InsertAsync(mapper.Map<Address>(addressCreateModel));
        await unitOfWork.SaveAsync();
        return mapper.Map<AddressViewModel>(existAddress);
    }
    public async ValueTask<AddressViewModel> UpdateAsync(long id, AddressUpdateModel addressUpdateModel)
    {
        var existAddress = await unitOfWork.Addresses.SelectAsync(address => address.Id == id)
             ?? throw new NotFoundException($"Address is not found with this id: {id}");

        existAddress.Street = addressUpdateModel.Street;
        existAddress.City = addressUpdateModel.City;
        existAddress.HouseNumber = addressUpdateModel.HouseNumber;
        existAddress.Latitude = addressUpdateModel.Latitude;
        existAddress.Longitude = addressUpdateModel.Longitude;
        existAddress.PostCode = addressUpdateModel.PostCode;
        existAddress.UpdatedAt = DateTime.UtcNow;
        existAddress.UpdatedByUserId = HttpContextHelper.UserId;

        await unitOfWork.Addresses.UpdateAsync(existAddress);
        await unitOfWork.SaveAsync();
        return mapper.Map<AddressViewModel>(existAddress);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existAddress = await unitOfWork.Addresses.SelectAsync(address => address.Id == id)
             ?? throw new NotFoundException($"Address is not found with this id: {id}");
        await unitOfWork.Addresses.DropAsync(existAddress);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<AddressViewModel> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var addresses = unitOfWork.Addresses.SelectAsQueryable().OrderBy(filter);
        if (!string.IsNullOrEmpty(search))
            addresses = addresses.Where(f => f.Street.Contains(search, StringComparison.OrdinalIgnoreCase));

        return (AddressViewModel)await Task.FromResult(mapper.Map<IEnumerable<AddressViewModel>>(addresses.ToPaginate(@params)));
    }

    public async ValueTask<AddressViewModel> GetByIdAsync(long id)
    {
        var existAddress = await unitOfWork.Addresses.SelectAsync(address => address.Id == id)
             ?? throw new NotFoundException($"Address is not found with this id: {id}");
        return  mapper.Map<AddressViewModel>(existAddress);
    }
}