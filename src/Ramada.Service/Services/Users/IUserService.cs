using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Auths;
using Ramada.Service.DTOs.Users;

namespace Ramada.Service.Services.Users;

public interface IUserService
{
    ValueTask<LogInViewModel> LogInAsync(LogInCreateModel model);
    ValueTask<UserViewModel> CreateAsync(UserCreateModel user);
    ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel user);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params,
                                                      Filter filter,
                                                      string search = null);
}
