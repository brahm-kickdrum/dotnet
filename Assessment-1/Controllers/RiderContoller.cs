using Assessment_1.Enums;
using Assessment_1.Exceptions;
using Assessment_1.Models;
using Assessment_1.Services.IServices;
using Assessment_1.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_1.Controllers
{
    [Route("api/rider")]
    [ApiController]
    public class RiderContoller : ControllerBase
    {
        private IRiderService _riderService;

        public RiderContoller(IRiderService riderService)
        {
            _riderService = riderService;
        }

        [Authorize(Roles = "Rider")]
        [HttpPost("request-trip")]
        public ActionResult<TripRequestResponse> RequestTrip(TripRequest tripRequest)
        {
            string? userEmail = JwtTokenUtils.ExtractEmail(HttpContext);
            UserRole? userRole = JwtTokenUtils.ExtractRole(HttpContext);

            if (userEmail == null || userRole == null)
            {
                return BadRequest("Email or role not found in JWT token.");
            }

            try
            {
                TripRequestResponse tripRequestResponse = _riderService.RequestTrip(tripRequest, userEmail, (UserRole)userRole);
                return Ok(tripRequestResponse);
            }
            catch (DriverNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TripOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Rider")]
        [HttpGet("current-ride")]
        public ActionResult<RiderTripDetailsResponse> GetCurrentRide()
        {
            string? userEmail = JwtTokenUtils.ExtractEmail(HttpContext);

            if (userEmail == null)
            {
                return BadRequest("Email not found in JWT token.");
            }

            try
            {
                RiderTripDetailsResponse tripDetailsResponse = _riderService.GetCurrentTripDetails(userEmail);
                return Ok(tripDetailsResponse);
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DriverNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TripNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
