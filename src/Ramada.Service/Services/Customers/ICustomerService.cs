using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Customers;

namespace Ramada.Service.Services.Customers;

public interface ICustomerService
{
    ValueTask<CustomerViewModel> CreateAsync(CustomerCreateModel customer);
    ValueTask<CustomerViewModel> UpdateAsync(long id, CustomerUpdateModel customer);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<CustomerViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<CustomerViewModel>> GetAllAsync(PaginationParams @params,
                                                          Filter filter,
                                                          string search = null);
}
