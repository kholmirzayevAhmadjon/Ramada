using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Customers;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Customers;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;
using Ramada.Service.Services.Assets;
using Ramada.Service.Services.Users;

namespace Ramada.Service.Services.Customers;

public class CustomerService(IUnitOfWork unitOfWork,
                             IUserService userService,
                             IAssetService assetService,
                             IMapper mapper) : ICustomerService
{
    public async ValueTask<CustomerViewModel> CreateAsync(CustomerCreateModel customer)
    {
        var asset = await assetService.GetByIdAsync(customer.AssetId ?? 0);
        var user = await userService.GetByIdAsync(customer.UserId);

        var mappedCustomer = mapper.Map<Customer>(customer);
        mappedCustomer.CreatedByUserId = HttpContextHelper.UserId;

        var createdCustomer = await unitOfWork.Customers.InsertAsync(mappedCustomer);
        await unitOfWork.SaveAsync();

        var result = mapper.Map<CustomerViewModel>(createdCustomer);
        result.User = user;
        result.Asset = asset;

        return result;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var customer = await unitOfWork.Customers.SelectAsync(c => c.Id == id && !c.IsDeleted)
            ?? throw new NotFoundException($"Customer is not found with this id: {id}");

        customer.DeletedByUserId = HttpContextHelper.UserId;
        var deletedCustomer = await unitOfWork.Customers.DeleteAsync(customer);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<CustomerViewModel>> GetAllAsync(PaginationParams @params,
                                                                       Filter filter,
                                                                       string search = null)
    {
        var customers = unitOfWork.Customers.SelectAsQueryable(c => !c.IsDeleted, ["Bookings", "User", "Asset"], false);

        if (!string.IsNullOrWhiteSpace(search))
            customers = customers.Where(c =>
                c.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                c.LastName.Contains(search, StringComparison.OrdinalIgnoreCase));

        var result = await customers.ToPaginate(@params).ToListAsync();

        return mapper.Map<IEnumerable<CustomerViewModel>>(result);
    }

    public async ValueTask<CustomerViewModel> GetByIdAsync(long id)
    {
        var customer = await unitOfWork.Customers.SelectAsync(c => c.Id == id && !c.IsDeleted, ["Bookings", "User", "Asset"])
            ?? throw new NotFoundException($"Customer is not found with this id: {id}");

        return mapper.Map<CustomerViewModel>(customer);
    }

    public async ValueTask<CustomerViewModel> UpdateAsync(long id, CustomerUpdateModel customer)
    {
        var existCustomer = await unitOfWork.Customers.SelectAsync(c => c.Id == id && !c.IsDeleted)
            ?? throw new NotFoundException($"Customer is not found with this id: {id}");
        var asset = await assetService.GetByIdAsync(customer.AssetId ?? 0);
        var user = await userService.GetByIdAsync(customer.UserId);

        mapper.Map(customer, existCustomer);
        existCustomer.UpdatedByUserId = HttpContextHelper.UserId;

        var updatedCustomer = await unitOfWork.Customers.UpdateAsync(existCustomer);
        await unitOfWork.SaveAsync();

        var result = mapper.Map<CustomerViewModel>(updatedCustomer);
        result.User = user;
        result.Asset = asset;
        
        return result;
    }
}
