using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Domain.Entities.Users;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Auths;
using Ramada.Service.DTOs.Users;
using Ramada.Service.Exceptions;
using Ramada.Service.Extensions;
using Ramada.Service.Helpers;
using Ramada.Service.Services.Auths;

namespace Ramada.Service.Services.Users;

public class UserService(IUnitOfWork unitOfWork,
                         IAuthService authService,
                         IMapper mapper) : IUserService
{
    public async ValueTask<UserViewModel> CreateAsync(UserCreateModel user)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => !u.IsDeleted
            && (u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase) 
                       || u.Phone.Equals(user.Phone)));

        if (existUser is not null)
            throw new AlreadyExistException($"User already exists with this phone: {user.Phone} or email: {user.Email}");

        var mappedUser = mapper.Map<User>(user);
        mappedUser.CreatedByUserId = HttpContextHelper.UserId;
        mappedUser.Password = PasswordHasher.Hash(user.Password);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var user = await unitOfWork.Users.SelectAsync(u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException($"User is not found with this id: {id}");

        user.DeletedByUserId = HttpContextHelper.UserId;
        var deletedUser = await unitOfWork.Users.DeleteAsync(user);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params,
                                                                   Filter filter,
                                                                   string search = null)
    {
        var users = unitOfWork.Users
            .SelectAsQueryable(expression: u => !u.IsDeleted, isTracked: false)
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            users = users.Where(u => u.Role.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        var result = await users.ToPaginate(@params).ToListAsync();

        return mapper.Map<IEnumerable<UserViewModel>>(result);
    }

    public async ValueTask<UserViewModel> GetByIdAsync(long id)
    {
        var user = await unitOfWork.Users.SelectAsync(u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException($"User is not found with this id: {id}");

        return mapper.Map<UserViewModel>(user);
    }

    public async ValueTask<LogInViewModel> LogInAsync(LogInCreateModel model)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel user)
    {
        throw new NotImplementedException();
    }
}
