using Ramada.Domain.Entities.Users;

namespace Ramada.Service.Services.Auths;

public interface IAuthService
{
    string GenerateToken(User user);
}
