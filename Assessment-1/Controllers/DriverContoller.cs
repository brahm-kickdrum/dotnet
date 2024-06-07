using Assessment_1.Enums;
using Assessment_1.Exceptions;
using Assessment_1.Mappers;
using Assessment_1.Models;
using Assessment_1.Services.IServices;
using Assessment_1.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_1.Controllers
{
    [Route("api/driver")]
    [ApiController]
    public class DriverContoller : ControllerBase
    {
        private IDriverService _driverService;

        public DriverContoller(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [Authorize(Roles = "Driver")]
        [HttpPut("toggle-availability")]
        public ActionResult<DriverAvailability> ToggleAvailability()
        {
            string? userEmail = JwtTokenUtils.ExtractEmail(HttpContext);

            if (userEmail == null )
            {
                return BadRequest("Email not found in JWT token.");
            }

            try
            {
                DriverAvailability driverAvailability = _driverService.ToggleAvailability(userEmail);
                return Ok(driverAvailability);
            }
            catch (DriveNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FailedToUpdateDriverAvailabilityException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
