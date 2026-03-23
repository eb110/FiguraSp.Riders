using Figurasp.Riders.Service.Services;
using FiguraSp.Riders.Entity;
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
            var roles = await riderService.GetRiders();
            return Ok(roles);
        }
    }
}
