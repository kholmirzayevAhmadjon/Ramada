    using global::Ramada.Service.Configurations;
    using global::Ramada.Service.DTOs.RoomFacilities;
    using global::Ramada.Service.Services.RoomFacilities;
    using global::Ramada.WebApi.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace Ramada.WebApi.Controllers;

    public class RoomFacilitiesController(IRoomFacilityService roomFacilityService) : BaseController
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
                Data = await roomFacilityService.GetAllAsync(@params, filter, search)
            });
        }

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute] long id)
        {
            return Ok(new Response()
            {
                Message = "Ok",
                StatusCode = 200,
                Data = await roomFacilityService.GetByIdAsync(id)
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public async ValueTask<IActionResult> PostAsync([FromBody] RoomFacilityCreateModel model)
        {
            return Ok(new Response()
            {
                Message = "Ok",
                StatusCode = 200,
                Data = await roomFacilityService.CreateAsync(model)
            });
        }

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> PutAsync([FromRoute] long id,
                                                  [FromBody] RoomFacilityUpdateModel model)
        {
            return Ok(new Response()
            {
                Message = "Ok",
                StatusCode = 200,
                Data = await roomFacilityService.UpdateAsync(id, model)
            });
        }

        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> DeleteAsync([FromRoute] long id)
        {
            return Ok(new Response()
            {
                Message = "Ok",
                StatusCode = 200,
                Data = await roomFacilityService.DeleteAsync(id)
            });
        }
    }


