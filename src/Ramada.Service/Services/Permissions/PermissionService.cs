using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Users;
using Ramada.Service.Configurations;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;

namespace Ramada.Service.Services.Permissions;

public class PermissionService(IUnitOfWork unitOfWork, IMapper mapper) : IPermissionService
{
    public async ValueTask<PermissionViewModel> CreateAsync(PermissionCreateModel permission)
    {
        var existPermission = await unitOfWork.Permissions
            .SelectAsync(p => p.Method.Equals(permission.Method, StringComparison.OrdinalIgnoreCase) &&
                              p.Controller.Equals(permission.Controller, StringComparison.OrdinalIgnoreCase));

        if (existPermission is not null)
            throw new AlreadyExistException("This kind of permission already exists");

        var mappedPermission = mapper.Map<Permission>(permission);
        mappedPermission.CreatedByUserId = HttpContextHelper.UserId;
        var createdPermission = await unitOfWork.Permissions.InsertAsync(mappedPermission);
        await unitOfWork.SaveAsync();

        return mapper.Map<PermissionViewModel>(createdPermission);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var permission = await unitOfWork.Permissions.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Permission is not found with this id: {id}");

        var deletedPermission = await unitOfWork.Permissions.DropAsync(permission);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<PermissionViewModel>> GetAllAsync(PaginationParams @params,
                                                                         Filter filter,
                                                                         string search = null)
    {
        var permissions = unitOfWork.Permissions.SelectAsQueryable(isTracked: false).OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            permissions = permissions.Where(p => p.Method.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                                 p.Controller.Contains(search, StringComparison.OrdinalIgnoreCase));

        var result = await permissions.ToPaginate(@params).ToListAsync();

        return mapper.Map<IEnumerable<PermissionViewModel>>(result);
    }

    public async ValueTask<PermissionViewModel> GetByIdAsync(long id)
    {
        var permission = await unitOfWork.Permissions.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Permission is not found with this id: {id}");

        return mapper.Map<PermissionViewModel>(permission);
    }

    public async ValueTask<PermissionViewModel> UpdateAsync(long id, PermissionUpdateModel permission)
    {
        var existPermission = await unitOfWork.Permissions.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Permission is not found with this id: {id}");

        var alreadyExistPermission = await unitOfWork.Permissions
           .SelectAsync(p => p.Method.Equals(permission.Method, StringComparison.OrdinalIgnoreCase) &&
                             p.Controller.Equals(permission.Controller, StringComparison.OrdinalIgnoreCase));

        if (alreadyExistPermission is not null)
            throw new AlreadyExistException("This kind of permission already exists");

        var mappedPermission = mapper.Map(permission, existPermission);
        mappedPermission.UpdatedByUserId = HttpContextHelper.UserId;
        var updatedPermission = await unitOfWork.Permissions.UpdateAsync(mappedPermission);
        await unitOfWork.SaveAsync();

        return mapper.Map<PermissionViewModel>(updatedPermission);
    }
}
