using Microsoft.AspNetCore.Mvc;
using Ramada.Service.Configurations;
using Ramada.Service.DTOs.Assets;
using Ramada.Service.DTOs.Bookings;
using Ramada.Service.Services.Assets;
using Ramada.WebApi.Models;

namespace Ramada.WebApi.Controllers;

public class AssetsController(IAssetService service) : BaseController
{

    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await service.GetByIdAsync(id)
        });
    }

    [HttpPost]
    public async ValueTask<IActionResult> PostAsync(IFormFile file, FileType type)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await service.UploadAsync(file, type)
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
}
