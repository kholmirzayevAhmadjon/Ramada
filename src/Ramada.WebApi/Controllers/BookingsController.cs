using Microsoft.AspNetCore.Mvc;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Bookings;
using Ramada.Service.Services.Bookings;
using Ramada.WebApi.Models;

namespace Ramada.WebApi.Controllers;

public class BookingsController(IBookingService service) : BaseController
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAsync([FromQuery] PaginationParams @params,
                                          [FromQuery] Filter filter,
                                          [FromQuery] string search)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await service.GetAllAsync(@params, filter, search)
        });
    }

    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode= 200,
            Message = "Success",
            Data = await service.GetAsync(id)
        });
    }

    [HttpPost]
    public async ValueTask<IActionResult> PostAsync(BookingCreateModel model)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await service.CreateAsync(model)
        });
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(int id, BookingUpdateModel model)
    {
        return Ok(new Response
        {
           StatusCode = 200,
           Message = "Success",
           Data = await service.UpdateAsync(id, model) 
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await service.DeleteAsync(id)
        });
    }

    [HttpPatch("{id:long}")]
    public async ValueTask<IActionResult> FinishBooking(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await service.FinishBooking(id)
        });
    }

}
