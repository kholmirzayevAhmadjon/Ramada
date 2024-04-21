using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Ramada.Domain.Entities.Users;
using Ramada.Service.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ramada.Service.Services.Auths;

public class AuthService(IOptions<JwtOption> options) : IAuthService
{
    private readonly JwtOption jwtOption = options.Value;
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(jwtOption.Key);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Audience = jwtOption.Audience,
            Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(jwtOption.LifeTime)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwtOption.Issuer,
            Subject = new ClaimsIdentity(new Claim[]
            {
                new("Id", user.Id.ToString()),
                new("Phone", user.Phone),
                new(ClaimTypes.Role, user.Role.Name)
            })
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
