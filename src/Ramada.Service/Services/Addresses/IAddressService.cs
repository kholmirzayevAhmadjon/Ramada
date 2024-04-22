
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Addresses;

namespace Ramada.Service.Services.Addresses;

public interface IAddressService
{
    ValueTask<AddressViewModel> CreateAsync(AddressCreateModel addressCreateModel);
    ValueTask<AddressViewModel> UpdateAsync(long id,AddressUpdateModel addressUpdateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<AddressViewModel> GetByIdAsync(long id);
    ValueTask<AddressViewModel> GetAllAsync(PaginationParams @params,
                                                        Filter filter,
                                                        string search = null);
}
