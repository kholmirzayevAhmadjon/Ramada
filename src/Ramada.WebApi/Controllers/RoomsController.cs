using Microsoft.AspNetCore.Mvc;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Rooms;
using Ramada.Service.DTOs.Users;
using Ramada.Service.Services.Rooms;
using Ramada.Service.Services.Users;
using Ramada.WebApi.Models;

namespace Ramada.WebApi.Controllers;

public class RoomsController(IRoomService roomService) : BaseController
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
            Data = await roomService.GetAllAsync(@params, filter, search)
        });
    }

    [HttpGet("{id}")]
    public async ValueTask<IActionResult> GetAsync([FromRoute] long id)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await roomService.GetByIdAsync(id)
        });
    }

    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] RoomCreateModel room)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await roomService.CreateAsync(room)
        });
    }

    [HttpPut("{id}")]
    public async ValueTask<IActionResult> PutAsync([FromRoute] long id,
                                              [FromBody] RoomUpdateModel room)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await roomService.UpdateAsync(id, room)
        });
    }

    [HttpDelete("{id}")]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] long id)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await roomService.DeleteAsync(id)
        });
    }
}
