using Assignment_3.Dtos;
using Assignment_3.Exceptions;
using Assignment_3.Mappers;
using Assignment_3.Services;
using Assignment_3.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_3.Controllers
{
    [Route("api/rent")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpPost("id")]
        public ActionResult<RentalIdResponseViewModel> RentMovieById(RentMovieByIdRequestViewModel rentMovieRequestViewModel)
        {
            Guid rentalId = _rentalService.RentMovieById(rentMovieRequestViewModel.MovieId, rentMovieRequestViewModel.CustomerId);
            return Ok(RentalMapper.CreateRentalIdResponseViewModelFromRentalId(rentalId));
        }

        [HttpPost]
        public ActionResult<RentalIdResponseViewModel> RentMovieByMovieTitleAndUsername(RentMovieByMovieNameAndUserNameRequestViewModel rentMovieByMovieNameAndUserNameRequestViewModel)
        {
            Guid rentalId = _rentalService.RentMovieByMovieTitleAndUsername(rentMovieByMovieNameAndUserNameRequestViewModel.MovieTitle, rentMovieByMovieNameAndUserNameRequestViewModel.CustomerUsername);
            return Ok(RentalMapper.CreateRentalIdResponseViewModelFromRentalId(rentalId));
        }

        [HttpGet("movie-id/{id}")]
        public ActionResult<List<string>> GetCustomersByMovieId(Guid id)
        {
            List<string> customerList = _rentalService.GetCustomersByMovieId(id);
            return Ok(customerList);
        }

        [HttpGet("customer-id/{id}")]
        public ActionResult<List<string>> GetMoviesByCustomerId(Guid id)
        {
            List<string> movieList = _rentalService.GetMoviesByCustomerId(id);
            return Ok(movieList);
        }

        [HttpGet("cost/{id}")]
        public ActionResult<decimal> GetTotalCost(Guid id)
        {
            decimal totalCost = _rentalService.GetTotalCostByCustomerId(id);
            return Ok(totalCost);
        }
    }
}
