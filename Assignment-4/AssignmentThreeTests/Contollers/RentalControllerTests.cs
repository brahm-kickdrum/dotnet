using Assignment_3.Controllers;
using Assignment_3.Dtos;
using Assignment_3.Exceptions;
using Assignment_3.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentThreeTests.Contollers
{
    public class RentalControllerTests
    {
        private readonly Mock<IRentalService> _mockRentalService;
        private readonly RentalController _controller;

        public RentalControllerTests()
        {
            _mockRentalService = new Mock<IRentalService>();
            _controller = new RentalController(_mockRentalService.Object);
        }

        [Fact]
        public void RentMovieById_ReturnsOkResult_WithRentalId()
        {
            // Arrange
            RentMovieByIdRequestViewModel rentMovieRequestViewModel = new RentMovieByIdRequestViewModel
            {
                MovieId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid()
            };
            Guid rentalId = Guid.NewGuid();
            _mockRentalService.Setup(service => service.RentMovieById(rentMovieRequestViewModel.MovieId, rentMovieRequestViewModel.CustomerId)).Returns(rentalId);

            // Act
            ActionResult<RentalIdResponseViewModel> result = _controller.RentMovieById(rentMovieRequestViewModel);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            RentalIdResponseViewModel model = Assert.IsType<RentalIdResponseViewModel>(okResult.Value);
            Assert.Equal(rentalId, model.RentalId);
        }

        [Fact]
        public void RentMovieByMovieTitleAndUsername_ReturnsOkResult_WithRentalId()
        {
            // Arrange
            RentMovieByMovieNameAndUserNameRequestViewModel rentMovieRequestViewModel = new RentMovieByMovieNameAndUserNameRequestViewModel
            {
                MovieTitle = "Test Movie",
                CustomerUsername = "TestUser"
            };
            Guid rentalId = Guid.NewGuid();
            _mockRentalService.Setup(service => service.RentMovieByMovieTitleAndUsername(rentMovieRequestViewModel.MovieTitle, rentMovieRequestViewModel.CustomerUsername)).Returns(rentalId);

            // Act
            ActionResult<RentalIdResponseViewModel> result = _controller.RentMovieByMovieTitleAndUsername(rentMovieRequestViewModel);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            RentalIdResponseViewModel model = Assert.IsType<RentalIdResponseViewModel>(okResult.Value);
            Assert.Equal(rentalId, model.RentalId);
        }

        [Fact]
        public void GetCustomersByMovieId_ReturnsOkResult_WithCustomerList()
        {
            // Arrange
            Guid movieId = Guid.NewGuid();
            List<string> customerList = new List<string> { "Customer1", "Customer2" };
            _mockRentalService.Setup(service => service.GetCustomersByMovieId(movieId)).Returns(customerList);

            // Act
            ActionResult<List<string>> result = _controller.GetCustomersByMovieId(movieId);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            List<string> model = Assert.IsType<List<string>>(okResult.Value);
            Assert.Equal(customerList, model);
        }

        [Fact]
        public void GetMoviesByCustomerId_ReturnsOkResult_WithMovieList()
        {
            // Arrange
            Guid customerId = Guid.NewGuid();
            List<string> movieList = new List<string> { "Movie1", "Movie2" };
            _mockRentalService.Setup(service => service.GetMoviesByCustomerId(customerId)).Returns(movieList);

            // Act
            ActionResult<List<string>> result = _controller.GetMoviesByCustomerId(customerId);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            List<string> model = Assert.IsType<List<string>>(okResult.Value);
            Assert.Equal(movieList, model);
        }

        [Fact]
        public void GetTotalCost_ReturnsOkResult_WithTotalCost()
        {
            // Arrange
            Guid customerId = Guid.NewGuid();
            decimal totalCost = 100;
            _mockRentalService.Setup(service => service.GetTotalCostByCustomerId(customerId)).Returns(totalCost);

            // Act
            ActionResult<decimal> result = _controller.GetTotalCost(customerId);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            decimal model = Assert.IsType<decimal>(okResult.Value);
            Assert.Equal(totalCost, model);
        }
    }
}
