using Microsoft.AspNetCore.Mvc;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Roles;
using Ramada.Service.Services.Roles;
using Ramada.WebApi.Models;

namespace Ramada.WebApi.Controllers;

public class RolesController(IRoleService roleService) : BaseController
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAsync([FromQuery] PaginationParams @params,
                                             [FromQuery] Filter filter,
                                             [FromQuery] string search)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await roleService.GetAllAsync(@params, filter, search)
        });
    }

    [HttpGet("{id}")]
    public async ValueTask<IActionResult> GetAsync([FromRoute] long id)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await roleService.GetByIdAsync(id)
        });
    }

    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] RoleCreateModel role)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await roleService.CreateAsync(role)
        });
    }

    [HttpPut("{id}")]
    public async ValueTask<IActionResult> PutAsync([FromRoute] long id,
                                             [FromBody] RoleUpdateeModel role)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await roleService.UpdateAsync(id, role)
        });
    }

    [HttpDelete("{id}")]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] long id)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await roleService.DeleteAsync(id)
        });
    }
}
