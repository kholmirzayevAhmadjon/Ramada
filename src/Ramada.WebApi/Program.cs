using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ramada.DataAccess.Contexts;
using Ramada.Service.Helpers;
using Ramada.Service.Mappers;
using Ramada.WebApi.Extensions;
using Ramada.WebApi.Helpers;
using Ramada.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options
    => options.Conventions
        .Add(new RouteTokenTransformerConvention(new RouteParameterTransformer()))).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenJwt();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAuthorization();
builder.Services.AddJwtService(builder.Configuration);
builder.Services.AddCustomValidators();
builder.Services.AddCustomServices();
EnvironmentHelper.WebRootPath = builder.Environment.WebRootPath;

var app = builder.Build();

using var scope = app.Services.CreateScope();
HttpContextHelper.HttpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
context.Database.EnsureCreated();
context.Database.Migrate();

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