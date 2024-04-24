using Microsoft.AspNetCore.Mvc;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Bookings;
using Ramada.Service.DTOs.Payments;
using Ramada.Service.Services.Payments;
using Ramada.WebApi.Models;

namespace Ramada.WebApi.Controllers;

public class PaymentsController(IPaymentService service) : BaseController
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAsync([FromQuery] PaginationParams @params)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await service.GetAll(@params)
        });
    }

    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await service.Get(id)
        });
    }

    [HttpPost]
    public async ValueTask<IActionResult> PostAsync(PaymentCreateModel model)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await service.Create(model)
        });
    }

  
    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await service.Delete(id)
        });
    }
}
