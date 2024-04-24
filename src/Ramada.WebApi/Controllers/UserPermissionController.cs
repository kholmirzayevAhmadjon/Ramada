using Ramada.Service.Configurations;
using Ramada.Service.Services.UserPermissions;
using Ramada.WebApi.Models;

namespace Ramada.WebApi.Controllers;

public class UserPermissionController(IUserPermissionService userPermissionService) : BaseController
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
            Data = await userPermissionService.GetAllAsync(@params, filter, search)
        });
    }

    [HttpGet("{id}")]
    public async ValueTask<IActionResult> GetAsync([FromRoute] long id)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await userPermissionService.GetByIdAsync(id)
        });
    }

    [AllowAnonymous]
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] UserPermissionController user)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await userPermissionService.CreateAsync(user)
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