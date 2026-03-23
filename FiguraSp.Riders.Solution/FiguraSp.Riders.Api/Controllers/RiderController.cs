using Figurasp.Riders.Service.Services;
using FiguraSp.Riders.Entity;
using FiguraSp.Riders.Model.DTOs.Requests;
using FiguraSp.Riders.Model.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FiguraSp.Riders.Api.Controllers
{
    [Route("api/[controller]")] // http://localhost:5000/api/rider
    [ApiController]
    public class RiderController(IRiderService riderService) : ControllerBase
    {
        [HttpGet]
        [Route("Riders")]
        public async Task<ActionResult<List<Rider>>> GetAllRiders()
        {
            var riders = await riderService.GetRiders();
            return Ok(riders);
        }

        [HttpGet]
        public async Task<ActionResult<Rider>> GetRiderById(Guid id)
        {
            var rider = await riderService.GetRiderById(id);
            return Ok(rider);
        }

        [HttpPut]
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
