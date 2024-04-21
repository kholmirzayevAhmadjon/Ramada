using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Roles;

namespace Ramada.Service.Services.RoleService;

public interface IRoleService
{
    ValueTask<RoleViewModel> Create(RoleCreateModel model);
    ValueTask<bool> Delete(long id);
    ValueTask<RoleViewModel> Update(long id, RoleUpdateeModel model);
    ValueTask<RoleViewModel> GetById(long id);
    ValueTask<IEnumerable<RoleViewModel>> GetAll(PaginationParams @params, Filter filter, string search = null);
}
