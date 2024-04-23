using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Auths;
using Ramada.Service.DTOs.Users;
using Ramada.Service.Services.Users;
using Ramada.WebApi.Models;

namespace Ramada.WebApi.Controllers;

public class UsersController(IUserService userService) : BaseController
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
            Data = await userService.GetAllAsync(@params, filter, search)
        });
    }

    [HttpGet("{id}")]
    public async ValueTask<IActionResult> GetAsync([FromRoute] long id)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await userService.GetByIdAsync(id)
        });
    }

    [AllowAnonymous]
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] UserCreateModel user)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await userService.CreateAsync(user)
        });
    }

    [HttpPut("{id}")]
    public async ValueTask<IActionResult> PutAsync([FromRoute] long id,
                                              [FromBody] UserUpdateModel user)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await userService.UpdateAsync(id, user)
        });
    }

    [HttpDelete("{id}")]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] long id)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await userService.DeleteAsync(id)
        });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async ValueTask<IActionResult> LogInAsync([FromBody] LogInCreateModel logIn)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await userService.LogInAsync(logIn)
        }); 
    }
}