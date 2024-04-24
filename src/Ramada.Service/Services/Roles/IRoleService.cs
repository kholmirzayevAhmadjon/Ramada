using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Roles;

namespace Ramada.Service.Services.Roles;

public interface IRoleService
{
    ValueTask<RoleViewModel> CreateAsync(RoleCreateModel model);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<RoleViewModel> UpdateAsync(long id, RoleUpdateeModel model);
    ValueTask<RoleViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<RoleViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
