using Ramada.Domain.Entities.Users;
using Ramada.Service.Configurations;

namespace Ramada.Service.Services.Permissions;

public interface IPermissionService
{
    ValueTask<PermissionViewModel> CreateAsync(PermissionCreateModel permission);
    ValueTask<PermissionViewModel> UpdateAsync(long id, PermissionUpdateModel permission);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<PermissionViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<PermissionViewModel>> GetAllAsync(PaginationParams @params,
                                                            Filter filter,
                                                            string search = null);
}
