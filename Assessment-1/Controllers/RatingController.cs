using Assessment_1.Enums;
using Assessment_1.Exceptions;
using Assessment_1.Models;
using Assessment_1.Services.IServices;
using Assessment_1.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_1.Controllers
{
    [Route("api/rating")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [Authorize(Roles = "Driver")]
        [HttpPut("rider")]
        public ActionResult<string> RateRider(RatingRequest ratingRequest)
        {
            string? userEmail = JwtTokenUtils.ExtractEmail(HttpContext);
            UserRole? userRole = JwtTokenUtils.ExtractRole(HttpContext);

            if (userEmail == null || userRole == null)
            {
                return BadRequest("Email or role not found in JWT token.");
            }

            try
            {
                _ratingService.UpdateRiderRating(ratingRequest, userEmail, (UserRole) userRole);
                return Ok("Rider rated successfully.");
            }
            catch (RatingNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TripNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BookingNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Rider")]
        [HttpPut("driver")]
        public ActionResult<string> RateDriver(RatingRequest ratingRequest)
        {
            string? userEmail = JwtTokenUtils.ExtractEmail(HttpContext);
            UserRole? userRole = JwtTokenUtils.ExtractRole(HttpContext);

            if (userEmail == null || userRole == null)
            {
                return BadRequest("Email or role not found in JWT token.");
            }

            try
            {
                _ratingService.UpdateDriverRating(ratingRequest, userEmail, (UserRole)userRole);
                return Ok("Driver rated successfully.");
            }
            catch (RatingNotFoundException ex)
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
