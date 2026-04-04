using Figurasp.Riders.Service.Services;
using FiguraSp.Riders.Model.DTOs.Requests;
using FiguraSp.Riders.Model.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiguraSp.Riders.Api.Controllers
{
    [Route("api/[controller]")] // http://localhost:5000/api/rider
    [ApiController]
    public class RiderController(IRiderService riderService) : ControllerBase
    {
        [HttpGet]
        [Route("Riders")]
        //comes directly from the shared library, so it is protected by the jwt scheme, so we need to be authorized to access it
        public async Task<ActionResult<List<RiderResponseDto>>> GetAllRiders()
        {
            var riders = await riderService.GetRiders();
            return Ok(riders);
        }

        [HttpGet]
        public async Task<ActionResult<RiderResponseDto>> GetRiderById(Guid id)
        {
            var result = await riderService.GetRiderById(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [Route("SeasonRiders")]
        public async Task<ActionResult<List<RiderResponseDto>>> SeasonRiders(DateOnly year)
        {
            var result = await riderService.GetSeasonRiders(year);

            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<RiderResponseDto>> UpdateRider([FromBody] UpdateRiderRequestDto updateRider)
        {
            var response = await riderService.UpdateRider(updateRider);
            if (!response.Success)
            {
                return NotFound("Rider not found.");
            }
            return Ok();
        }

        [HttpGet]
        [Route("RiderByInitials")]
        public async Task<ActionResult<RiderResponseDto>> GetRiderByInitials(string name, string surname)
        {
            var rider = await riderService.GetRiderByInitials(name, surname);
            if(!rider.Success)
            {
                return NotFound("Rider not found.");
            }
            return Ok(rider);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<RiderResponseDto>> AddRider([FromBody] NewRiderRequestDto newRider)
        {
            var response = await riderService.AddRider(newRider);

            if(!response.Success)
            {
                return BadRequest("Rider with the same name and surname already exists.");
            }

            return CreatedAtAction("GetRiderByInitials", new { newRider.Name, newRider.Surname }, response);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteRider(Guid id)
        {
            var response = await riderService.RemoveRider(id);
            if (!response.Success)
            {
                return NotFound("Rider not found.");
            }
            return NoContent();
        }
    }
}
