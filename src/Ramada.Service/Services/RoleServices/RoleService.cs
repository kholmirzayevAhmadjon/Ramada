using AutoMapper;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Users;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Roles;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;

namespace Ramada.Service.Services.RoleService;

public class RoleService(IUnitOfWork unitOfWork, IMapper mapper) : IRoleService
{
    public async ValueTask<RoleViewModel> CreateAsync(RoleCreateModel model)
    {
        var role = await unitOfWork.Roles.SelectAsync(r => r.Name.ToLower() == model.Name.ToLower());
        if (role is not null)
        {
            if (!role.IsDeleted)
            {
                await UpdateAsync(role.Id, mapper.Map<RoleUpdateeModel>(role));
            }
            throw new AlreadyExistException("This role already exists");

        }
        var result = mapper.Map<Role>(model);
        result.CreatedByUserId = HttpContextHelper.UserId;
        var createRole = await unitOfWork.Roles.InsertAsync(result);
        await unitOfWork.SaveAsync();
        return mapper.Map<RoleViewModel>(createRole);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var role = await unitOfWork.Roles.SelectAsync(expression: role => role.Id == id && !role.IsDeleted)
            ?? throw new NotFoundException("This role is not found");

        role.DeletedAt = DateTime.UtcNow;
        role.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Roles.DeleteAsync(role);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<RoleViewModel> UpdateAsync(long id, RoleUpdateeModel model)
    {
        var role = await unitOfWork.Roles.SelectAsync(expression: role => role.Id == id && !role.IsDeleted)
            ?? throw new NotFoundException("This role is not found");

        var existRoles = await unitOfWork.Roles.SelectAsync(expression: role => role.Name.ToLower() == model.Name.ToLower());
        if (existRoles is not null)
        {
            throw new AlreadyExistException("This role already exists");
        }
        if (!existRoles.IsDeleted)
        {
            existRoles.IsDeleted = false;
        }
        existRoles.Name = model.Name;
        existRoles.UpdatedByUserId = HttpContextHelper.UserId;
        existRoles.UpdatedAt = DateTime.UtcNow;
        await unitOfWork.Roles.UpdateAsync(existRoles);
        await unitOfWork.SaveAsync();
        return mapper.Map<RoleViewModel>(existRoles);
    }

    public async ValueTask<RoleViewModel> GetByIdAsync(long id)
    {
        var role = await unitOfWork.Roles.SelectAsync(expression: role => role.Id == id && !role.IsDeleted)
            ?? throw new NotFoundException("This role is not found");

        return mapper.Map<RoleViewModel>(role);
    }

    public async ValueTask<IEnumerable<RoleViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var roles = unitOfWork.Roles.SelectAsQueryable().OrderBy(filter);
        if (!string.IsNullOrEmpty(search))
            roles = roles.Where(role => role.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await Task.FromResult(mapper.Map<IEnumerable<RoleViewModel>>(roles.ToPaginate(@params)));
    }
}