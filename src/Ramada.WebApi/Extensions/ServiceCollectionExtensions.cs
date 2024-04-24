using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Service.DTOs.Users;
using Ramada.Service.Options;
using Ramada.Service.Services.Addresses;
using Ramada.Service.Services.Assets;
using Ramada.Service.Services.Auths;
using Ramada.Service.Services.Bookings;
using Ramada.Service.Services.Customers;
using Ramada.Service.Services.Facilities;
using Ramada.Service.Services.Hostels;
using Ramada.Service.Services.Payments;
using Ramada.Service.Services.Permissions;
using Ramada.Service.Services.Roles;
using Ramada.Service.Services.RoomAssets;
using Ramada.Service.Services.RoomFacilities;
using Ramada.Service.Services.Rooms;
using Ramada.Service.Services.UserPermissions;
using Ramada.Service.Services.Users;
using Ramada.Service.Validators.Addresses;
using Ramada.Service.Validators.Bookings;
using Ramada.Service.Validators.Customers;
using Ramada.Service.Validators.Facilities;
using Ramada.Service.Validators.Hostels;
using Ramada.Service.Validators.Payments;
using Ramada.Service.Validators.Permissions;
using Ramada.Service.Validators.Roles;
using Ramada.Service.Validators.RoomAssets;
using Ramada.Service.Validators.RoomFacilities;
using Ramada.Service.Validators.Rooms;
using Ramada.Service.Validators.UserPermissions;
using Ramada.Service.Validators.Users;
using System.Text;

namespace Ramada.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCustomValidators(this IServiceCollection services)
    {
        services.AddTransient<UserCreateModelValidator>();
        services.AddTransient<UserUpdateModelValidator>();

        services.AddTransient<RoomFacilityCreateModelValidator>();
        services.AddTransient<RoomFacilityUpdateModelValidator>();
        
        services.AddTransient<RoleCreateModelValidator>();
        services.AddTransient<RoleUpdateModelValidator>();
       
        services.AddTransient<HostelCreateModelValidator>();
        services.AddTransient<HostelUpdateModelValidator>();
       
        services.AddTransient<AddressCreateModelValidator>();
        services.AddTransient<AddressUpdateModelValidator>();
       
        services.AddTransient<BookingCreateModelValidator>();
        services.AddTransient<BookingUpdateModelValidator>();
        
        services.AddTransient<CustomerCreateModelValidator>();
        services.AddTransient<CustomerUpdateModelValidator>();

        services.AddTransient<FacilityCreateModelValidator>();
        services.AddTransient<FacilityUpdateModelValidator>();

        services.AddTransient<PaymentCreateModelValidator>();

        services.AddTransient<PermissionCreateModelValidator>();
        services.AddTransient<PermissionUpdateModelValidator>();

        services.AddTransient<RoomAssetCreateModelValidator>();

        services.AddTransient<RoomCreateModelValidator>();
        services.AddTransient<RoomUpdateModelValidator>();

        services.AddTransient<UserPermissionCreateModelValidator>();
    }

    public static void AddCustomServices(this IServiceCollection services)
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
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IRoomAssetService, RoomAssetService>();
        services.AddScoped<IUserPermissionService, UserPermissionService>();
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

