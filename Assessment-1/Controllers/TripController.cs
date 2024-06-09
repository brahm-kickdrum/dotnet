using Assessment_1.Enums;
using Assessment_1.Exceptions;
using Assessment_1.Models;
using Assessment_1.Services;
using Assessment_1.Services.IServices;
using Assessment_1.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_1.Controllers
{
    [Route("api/trip")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private ITripService _tripService;

        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [Authorize(Roles = "Rider,Driver")]
        [HttpDelete("cancel")]
        public ActionResult<string> CancelTrip(CancelTripRequest cancelTripRequest)
        {
            string? userEmail = JwtTokenUtils.ExtractEmail(HttpContext);
            UserRole? userRole = JwtTokenUtils.ExtractRole(HttpContext);

            if (userEmail == null || userRole == null)
            {
                return BadRequest("Email or role not found in JWT token.");
            }

            try
            {
                _tripService.CancelTrip(cancelTripRequest, userEmail, (UserRole)userRole);
                return Ok("Trip cancelled successfully.");
            }
            catch (TripNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DriverNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TripOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Rider,Driver")]
        [HttpDelete("end")]
        public ActionResult<string> EndTrip(EndTripRequest endTripRequest)
        {
            string? userEmail = JwtTokenUtils.ExtractEmail(HttpContext);
            UserRole? userRole = JwtTokenUtils.ExtractRole(HttpContext);

            if (userEmail == null || userRole == null)
            {
                return BadRequest("Email or role not found in JWT token.");
            }

            try
            {
                _tripService.EndTrip(endTripRequest, userEmail, (UserRole)userRole);
                return Ok("Trip ended successfully.");
            }
            catch (TripNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DriverNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TripOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (FailedToAddRatingsException ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }

        [Authorize(Roles = "Driver")]
        [HttpPost("start")]
        public ActionResult<string> StartTrip(StartTripRequest startTripRequest)
        {
            string? userEmail = JwtTokenUtils.ExtractEmail(HttpContext);
            UserRole? userRole = JwtTokenUtils.ExtractRole(HttpContext);

            if (userEmail == null || userRole == null)
            {
                return BadRequest("Email or role not found in JWT token.");
            }
            try
            {
                _tripService.StartRide(startTripRequest, userEmail, (UserRole)userRole);
                return Ok("Ride started successfully");
            }
            catch (TripNotFoundException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (TripOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (InvalidOtpException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(Roles = "Driver")]
        [HttpGet("current-ride")]
        public ActionResult<DriverTripDetailsResponse> GetCurrentRide()
        {
            string? userEmail = JwtTokenUtils.ExtractEmail(HttpContext);

            if (userEmail == null)
            {
                return BadRequest("Email not found in JWT token.");
            }

            try
            {
                DriverTripDetailsResponse tripDetailsResponse = _tripService.GetCurrentTripDetails(userEmail);
                return Ok(tripDetailsResponse);
            }
            catch (UserNotFoundException ex)
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
