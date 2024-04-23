using Microsoft.AspNetCore.Mvc;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Facilities;
using Ramada.Service.Services.Facilities;
using Ramada.WebApi.Models;

namespace Ramada.WebApi.Controllers;

public class FacilitiesController(IFacilityService facilityService) : BaseController
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
            Data = await facilityService.GetAllAsync(@params, filter, search)
        });
    }

    [HttpGet("{id}")]
    public async ValueTask<IActionResult> GetAsync([FromRoute] long id)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await facilityService.GetByIdAsync(id)
        });
    }

    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] FacilityCreateModel facilityCreateModel)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await facilityService.CreateAsync(facilityCreateModel)
        });
    }

    [HttpPut("{id}")]
    public async ValueTask<IActionResult> PutAsync([FromRoute] long id,
                                              [FromBody] FacilityUpdateModel facilityUpdateModel)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await facilityService.UpdateAsync(id, facilityUpdateModel)
        });
    }

    [HttpDelete("{id}")]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] long id)
    {
        return Ok(new Response()
        {
            Message = "Ok",
            StatusCode = 200,
            Data = await facilityService.DeleteAsync(id)
        });
    }
}
