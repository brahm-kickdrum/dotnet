using Assignment_3.Dtos;
using Assignment_3.Exceptions;
using Assignment_3.Mappers;
using Assignment_3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_3.Controllers
{
    [Route("api/rent")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly RentalService _rentalService;

        public RentalController(RentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpPost("id")]
        public ActionResult<string> RentMovieById(RentMovieByIdRequestDto rentMovieRequestDto)
        {
            try
            {
                Guid RentalId = _rentalService.RentMovieById(rentMovieRequestDto.MovieId, rentMovieRequestDto.CustomerId);
                return Ok(RentalId);
            }
            catch (MovieNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (RentalAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (FailedToAddRentalException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<string> RentMovieByMovieTitleAndUsername(RentMovieByMovieNameAndUserNameRequestDto rentMovieByMovieNameAndUserNameRequestDto)
        {
            try
            {
                Guid RentalId = _rentalService.RentMovieByMovieTitleAndUsername(rentMovieByMovieNameAndUserNameRequestDto.MovieTitle, rentMovieByMovieNameAndUserNameRequestDto.CustomerUsername);
                return Ok(RentalId);
            }
            catch (MovieNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (RentalAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (FailedToAddRentalException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("movie-id/{id}")]
        public ActionResult<List<string>> GetCustomersByMovieId(Guid id)
        {
            try
            {
                List<string> CustomerList = _rentalService.GetCustomersByMovieId(id);
                return Ok(CustomerList);
            }
            catch (MovieNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("customer-id/{id}")]
        public ActionResult<List<string>> GetMoviesByCustomerId(Guid id)
        {
            try
            {
                List<string> CustomerList = _rentalService.GetMoviesByCustomerId(id);
                return Ok(CustomerList);
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("cost/{id}")]
        public ActionResult<decimal> GetTotalCost(Guid id)
        {
            try
            {
                decimal totalCost = _rentalService.GetTotalCostByCustomerId(id);
                return Ok(totalCost);
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
