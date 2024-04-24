using Ramada.Domain.Entities.Users;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.UserPermissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramada.Service.Services.UserPermissions;

public interface IUserPermissionService
{
    ValueTask<UserPermissionViewModel> CreateAsync(UserPermissionCreateModel userPermissionCreate);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserPermissionViewModel> GetAsync(long id);
    ValueTask<IEnumerable<UserPermissionViewModel>> GetAllAsync(PaginationParams @params);
}
