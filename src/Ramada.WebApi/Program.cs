using Microsoft.EntityFrameworkCore;
using Ramada.DataAccess.Contexts;
using Ramada.DataAccess.UnitOfWorks;
using Ramada.Service.Helpers;
using Ramada.Service.Mappers;
using Ramada.Service.Options;
using Ramada.WebApi.Middlewares;
using Ramada.Service.Services.Auths;
using Ramada.Service.Services.RoleService;
using Ramada.Service.Services.Users;
using Ramada.WebApi.Extensions;
using Ramada.Service.Services.Facilities;
using Ramada.Service.Services.Rooms;
using Newtonsoft.Json;
using Ramada.Service.Services.Hostels;
using Ramada.Service.Services.Assets;
using Ramada.Service.Services.RoomFacilities;
using Ramada.Service.Services.Bookings;
using Ramada.Service.Services.Customers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenJwt();

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddJwtService(builder.Configuration);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFacilityService, FacilityService>();
builder.Services.AddScoped<IHostelService, HostelService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IRoomFacilityService, RoomFacilityService>();

EnvironmentHelper.WebRootPath = builder.Environment.WebRootPath;

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlerMiddleWare>();

app.Run();