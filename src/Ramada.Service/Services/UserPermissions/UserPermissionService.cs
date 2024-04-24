using AutoMapper;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Users;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.UserPermissions;
using Ramada.Service.DTOs.Users;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Validators.UserPermissions;

namespace Ramada.Service.Services.UserPermissions;

public class UserPermissionService(IMapper mapper,
                                   IUnitOfWork unitOfWork,
                                   UserPermissionCreateModelValidator userPermissionCreateModelValidator) : IUserPermissionService
{
    public async ValueTask<UserPermissionViewModel> CreateAsync(UserPermissionCreateModel userPermissionCreate)
    {
        await userPermissionCreateModelValidator.ValidateOrPanicAsync(userPermissionCreate);

        var permission = mapper.Map<UserPermissionn>(userPermissionCreate);

        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == userPermissionCreate.UserId)
            ?? throw new NotFoundException($"User with this Id is not found {userPermissionCreate.UserId}");
        var existPermission = await unitOfWork.Permissions.SelectAsync(p => p.Id == userPermissionCreate.PermissionId)
            ?? throw new NotFoundException($"Permission with this Id is not found {userPermissionCreate.PermissionId}");
        
        permission.User = existUser;
        permission.Permission = existPermission;

        await unitOfWork.UsersPermissions.InsertAsync(permission);
        await unitOfWork.SaveAsync();

        var mappedUserPermission = mapper.Map<UserPermissionViewModel>(permission);

        mappedUserPermission.Permission = mapper.Map<PermissionViewModel>(permission.Permission);
        mappedUserPermission.User = mapper.Map<UserViewModel>(permission.User);

        return mappedUserPermission;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var userPermission = await unitOfWork.UsersPermissions.SelectAsync(p => p.Id == id)
             ?? throw new NotFoundException($"Permission with this Id is not found {id}");

        await unitOfWork.UsersPermissions.DeleteAsync(userPermission);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<UserPermissionViewModel> GetAsync(long id)
    {
        var userPermission = await unitOfWork.UsersPermissions.SelectAsync(p => p.Id == id)
             ?? throw new NotFoundException($"Permission with this Id is not found {id}");

        var mappedUserPermission = mapper.Map<UserPermissionViewModel>(userPermission);

        mappedUserPermission.Permission = mapper.Map<PermissionViewModel>(userPermission.Permission);
        mappedUserPermission.User = mapper.Map<UserViewModel>(userPermission.User);

        return mappedUserPermission;
    }

    public async ValueTask<IEnumerable<UserPermissionViewModel>> GetAllAsync(PaginationParams @params)
    {
        var userPermission = unitOfWork.UsersPermissions.SelectAsQueryable(expression:
            u=>!u.IsDeleted, isTracked: false);

        var res = userPermission.ToPaginate(@params);

        var result = mapper.Map<IEnumerable<UserPermissionViewModel>>(res);

        return result;
    }
}