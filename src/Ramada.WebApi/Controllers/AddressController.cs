using Microsoft.AspNetCore.Mvc;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Addresses;
using Ramada.Service.Services.Addresses;
using Ramada.WebApi.Models;

namespace Ramada.WebApi.Controllers;

public class AddressController(IAddressService addressService) : BaseController
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
            Data = await addressService.GetAllAsync(@params, filter, search)
        });
    }

    [HttpGet("{id}")]
    public async ValueTask<IActionResult> GetAsync([FromRoute] long id)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await addressService.GetByIdAsync(id)
        });
    }

    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] AddressCreateModel addressCreateModel)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await addressService.CreateAsync(addressCreateModel)
        });
    }
}