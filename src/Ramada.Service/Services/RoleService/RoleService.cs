using AutoMapper;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Users;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Roles;
using Ramada.Service.Exceptions;
using Ramada.Service.Helpers;

namespace Ramada.Service.Services.RoleService;

public class RoleService(IUnitOfWork unitOfWork, IMapper mapper) : IRoleService
{
    public async ValueTask<RoleViewModel> Create(RoleCreateModel model)
    {
        var role = await unitOfWork.Roles.SelectAsync(r => r.Name.ToLower() == model.Name.ToLower() );
        if (role is not null)
        {
            if(!role.IsDeleted)
            {
                await Update(role.Id, mapper.Map<RoleUpdateeModel>(role));
            }
            throw new AlreadyExistException("This role already exists");

        }
        var result = mapper.Map<Role>(model);
        result.CreatedByUserId = HttpContextHelper.UserId;
        var createRole = await unitOfWork.Roles.InsertAsync(result);
        await unitOfWork.SaveAsync();
        return mapper.Map<RoleViewModel>(createRole);
    }

    public async ValueTask<bool> Delete(long id)
    {
        var role = await unitOfWork.Roles.SelectAsync(expression: role => role.Id == id && !role.IsDeleted)
            ?? throw new NotFoundException("This role is not found");

        role.DeletedAt = DateTime.UtcNow;
        role.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Roles.DeleteAsync(role);
        await unitOfWork.SaveAsync();
        return true;
    }

    public ValueTask<RoleViewModel> Update(long id, RoleUpdateeModel model)
    {
        throw new NotImplementedException();
    }
   
    public async ValueTask<RoleViewModel> GetById(long id)
    {
        var role = await unitOfWork.Roles.SelectAsync(expression: role => role.Id == id && !role.IsDeleted)
            ?? throw new NotFoundException("This role is not found");

        return mapper.Map<RoleViewModel>(role);
    }

    public ValueTask<IEnumerable<RoleViewModel>> GetAll(PaginationParams @params, Filter filter, string search = null)
    {
        throw new NotImplementedException();
    }

}