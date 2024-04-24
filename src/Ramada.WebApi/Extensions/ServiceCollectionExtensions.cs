using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ramada.DataAccess.Repositories;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Service.Options;
using Ramada.Service.Services.Addresses;
using Ramada.Service.Services.Assets;
using Ramada.Service.Services.Auths;
using Ramada.Service.Services.Bookings;
using Ramada.Service.Services.Customers;
using Ramada.Service.Services.Facilities;
using Ramada.Service.Services.Hostels;
using Ramada.Service.Services.Payments;
using Ramada.Service.Services.RoleService;
using Ramada.Service.Services.RoomFacilities;
using Ramada.Service.Services.Rooms;
using Ramada.Service.Services.Users;
using System.Text;

namespace Ramada.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IHostelService, HostelService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IFacilityService, FacilityService>();
        services.AddScoped<IRoomFacilityService, RoomFacilityService>();
        services.AddScoped<IAddressService, AddressService>();
    }
    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOption>(configuration.GetSection("JWT"));
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                ClockSkew = TimeSpan.Zero
            };
        });
    }

    public static void AddSwaggerGenJwt(this IServiceCollection services)
    {
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

            Reference = new OpenApiReference()
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });
        });
    }
}

