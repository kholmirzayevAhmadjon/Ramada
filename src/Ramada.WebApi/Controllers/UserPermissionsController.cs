using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.UserPermissions;
using Ramada.Service.Services.UserPermissions;
using Ramada.WebApi.Models;

namespace Ramada.WebApi.Controllers;

public class UserPermissionsController(IUserPermissionService userPermissionService) : BaseController
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAsync([FromQuery] PaginationParams @params)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await userPermissionService.GetAllAsync(@params)
        });
    }

    [HttpGet("{id}")]
    public async ValueTask<IActionResult> GetAsync([FromRoute] long id)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await userPermissionService.GetAsync(id)
        });
    }

    [AllowAnonymous]
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] UserPermissionCreateModel userPermission)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await userPermissionService.CreateAsync(userPermission)
        });
    }

    [HttpDelete("{id}")]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] long id)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await userPermissionService.DeleteAsync(id)
        });
    }
}