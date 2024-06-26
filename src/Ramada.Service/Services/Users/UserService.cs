﻿using AutoMapper;
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
using Ramada.Service.Services.Roles;
using Ramada.Service.Validators.UserPermissions;
using Ramada.Service.Validators.Users;

namespace Ramada.Service.Services.Users;

public class UserService(IUnitOfWork unitOfWork,
                         IAuthService authService,
                         IRoleService roleService,   
                         IMapper mapper,
                         UserCreateModelValidator userCreateModelValidator,
                         UserUpdateModelValidator userUpdateModelValidator): IUserService
{
    public async ValueTask<UserViewModel> CreateAsync(UserCreateModel user)
    {
        await userCreateModelValidator.ValidateOrPanicAsync(user);

        var existUser = await unitOfWork.Users.SelectAsync(u => 
            !u.IsDeleted
            && (u.Email.Equals(user.Email)
            || u.Phone.Equals(user.Phone)));
        var role = await roleService.GetByIdAsync(user.RoleId);

        if (existUser is not null)
            throw new AlreadyExistException($"User already exists with this phone: {user.Phone} or email: {user.Email}");

        var mappedUser = mapper.Map<User>(user);
        mappedUser.CreatedByUserId = HttpContextHelper.UserId;
        mappedUser.Password = PasswordHasher.Hash(user.Password);

        var createdUser = await unitOfWork.Users.InsertAsync(mappedUser);
        await unitOfWork.SaveAsync();

        var result = mapper.Map<UserViewModel>(createdUser);
        result.Role = role;

        return result;
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
            .SelectAsQueryable(expression: u => !u.IsDeleted, isTracked: false, includes: ["Role"])
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            users = users.Where(u => u.Role.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        var result = await users.ToPaginate(@params).ToListAsync();

        return mapper.Map<IEnumerable<UserViewModel>>(result);
    }

    public async ValueTask<UserViewModel> GetByIdAsync(long id)
    {
        var user = await unitOfWork.Users.SelectAsync(u => u.Id == id && !u.IsDeleted, ["Role"])
            ?? throw new NotFoundException($"User is not found with this id: {id}");

        return mapper.Map<UserViewModel>(user);
    }

    public async ValueTask<LogInViewModel> LogInAsync(LogInCreateModel model)
    {
        var user = await unitOfWork.Users
            .SelectAsync(u => !u.IsDeleted
                              && u.Phone.Equals(model.Phone),
                            ["Role"]) ??
            throw new ArgumentIsNotValidException("Phone or Password is not found");

        var isVerified = PasswordHasher.Verify(model.Password, user.Password);
        if (!isVerified)
            throw new NotFoundException("Phone or Password is not found");

        var token = authService.GenerateToken(user);
        var mappedUser = mapper.Map<UserViewModel>(user);

        return new LogInViewModel() { User = mappedUser, Token = token };
    }

    public async ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel user)
    {
        await userUpdateModelValidator.ValidateOrPanicAsync(user);

        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException($"User is not found with this id: {id}");
        var role = await roleService.GetByIdAsync(user.RoleId);

        var alreadyExistUser = await unitOfWork.Users.SelectAsync(u => !u.IsDeleted
            && (u.Email.Equals(user.Email)
                       || u.Phone.Equals(user.Phone)));

        if (alreadyExistUser is not null)
            throw new AlreadyExistException($"User already exists with this phone: {user.Phone} or email: {user.Email}");

        mapper.Map(user, existUser);
        existUser.UpdatedByUserId = HttpContextHelper.UserId;
        var updatedUser = await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        var result = mapper.Map<UserViewModel>(updatedUser);
        result.Role = role;

        return result;
    }
}
