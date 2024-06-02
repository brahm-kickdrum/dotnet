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
            RentMovieByIdRequestDto rentMovieRequestDto = new RentMovieByIdRequestDto
            {
                MovieId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid()
            };
            Guid rentalId = Guid.NewGuid();
            _mockRentalService.Setup(service => service.RentMovieById(rentMovieRequestDto.MovieId, rentMovieRequestDto.CustomerId)).Returns(rentalId);

            // Act
            ActionResult<RentalIdResponseDto> result = _controller.RentMovieById(rentMovieRequestDto);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            RentalIdResponseDto model = Assert.IsType<RentalIdResponseDto>(okResult.Value);
            Assert.Equal(rentalId, model.RentalId);
        }

        [Fact]
        public void RentMovieById_ReturnsNotFound_WhenMovieNotFound()
        {
            // Arrange
            RentMovieByIdRequestDto rentMovieRequestDto = new RentMovieByIdRequestDto
            {
                MovieId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid()
            };
            _mockRentalService.Setup(service => service.RentMovieById(rentMovieRequestDto.MovieId, rentMovieRequestDto.CustomerId)).Throws(new MovieNotFoundException("Movie does not exist"));

            // Act
            ActionResult<RentalIdResponseDto> result = _controller.RentMovieById(rentMovieRequestDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
            NotFoundObjectResult notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Movie does not exist", notFoundResult.Value);
        }

        [Fact]
        public void RentMovieById_ReturnsNotFound_WhenCustomerNotFound()
        {
            // Arrange
            RentMovieByIdRequestDto rentMovieRequestDto = new RentMovieByIdRequestDto
            {
                MovieId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid()
            };
            _mockRentalService.Setup(service => service.RentMovieById(rentMovieRequestDto.MovieId, rentMovieRequestDto.CustomerId)).Throws(new CustomerNotFoundException("Customer does not exist"));

            // Act
            ActionResult<RentalIdResponseDto> result = _controller.RentMovieById(rentMovieRequestDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
            NotFoundObjectResult notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Customer does not exist", notFoundResult.Value);
        }

        [Fact]
        public void RentMovieById_ReturnsConflict_WhenRentalAlreadyExists()
        {
            // Arrange
            RentMovieByIdRequestDto rentMovieRequestDto = new RentMovieByIdRequestDto
            {
                MovieId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid()
            };
            _mockRentalService.Setup(service => service.RentMovieById(rentMovieRequestDto.MovieId, rentMovieRequestDto.CustomerId)).Throws(new RentalAlreadyExistsException("A rental with the same Customer Id and Movie Id already exists."));

            // Act
            ActionResult<RentalIdResponseDto> result = _controller.RentMovieById(rentMovieRequestDto);

            // Assert
            Assert.IsType<ConflictObjectResult>(result.Result);
            ConflictObjectResult conflictResult = Assert.IsType<ConflictObjectResult>(result.Result);
            Assert.Equal("A rental with the same Customer Id and Movie Id already exists.", conflictResult.Value);
        }

        [Fact]
        public void RentMovieById_ReturnsInternalServerError_WhenFailedToAddRental()
        {
            // Arrange
            RentMovieByIdRequestDto rentMovieRequestDto = new RentMovieByIdRequestDto
            {
                MovieId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid()
            };
            _mockRentalService.Setup(service => service.RentMovieById(rentMovieRequestDto.MovieId, rentMovieRequestDto.CustomerId)).Throws(new FailedToAddRentalException("Failed to rent movie. An error occurred."));

            // Act
            ActionResult<RentalIdResponseDto> result = _controller.RentMovieById(rentMovieRequestDto);

            // Assert
            Assert.IsType<ObjectResult>(result.Result);
            ObjectResult internalServerErrorResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equal("Failed to rent movie. An error occurred.", internalServerErrorResult.Value);
        }

        [Fact]
        public void RentMovieByMovieTitleAndUsername_ReturnsOkResult_WithRentalId()
        {
            // Arrange
            RentMovieByMovieNameAndUserNameRequestDto rentMovieRequestDto = new RentMovieByMovieNameAndUserNameRequestDto
            {
                MovieTitle = "Test Movie",
                CustomerUsername = "TestUser"
            };
            Guid rentalId = Guid.NewGuid();
            _mockRentalService.Setup(service => service.RentMovieByMovieTitleAndUsername(rentMovieRequestDto.MovieTitle, rentMovieRequestDto.CustomerUsername)).Returns(rentalId);

            // Act
            ActionResult<RentalIdResponseDto> result = _controller.RentMovieByMovieTitleAndUsername(rentMovieRequestDto);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            RentalIdResponseDto model = Assert.IsType<RentalIdResponseDto>(okResult.Value);
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
        public void GetCustomersByMovieId_ReturnsNotFound_WhenMovieNotFound()
        {
            // Arrange
            Guid movieId = Guid.NewGuid();
            _mockRentalService.Setup(service => service.GetCustomersByMovieId(movieId)).Throws(new MovieNotFoundException("Movie does not exist"));

            // Act
            ActionResult<List<string>> result = _controller.GetCustomersByMovieId(movieId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
            NotFoundObjectResult notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Movie does not exist", notFoundResult.Value);
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
        public void GetMoviesByCustomerId_ReturnsNotFound_WhenCustomerNotFound()
        {
            // Arrange
            Guid customerId = Guid.NewGuid();
            _mockRentalService.Setup(service => service.GetMoviesByCustomerId(customerId)).Throws(new CustomerNotFoundException("Customer does not exist"));

            // Act
            ActionResult<List<string>> result = _controller.GetMoviesByCustomerId(customerId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
            NotFoundObjectResult notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Customer does not exist", notFoundResult.Value);
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

        [Fact]
        public void GetTotalCost_ReturnsNotFound_WhenCustomerNotFound()
        {
            // Arrange
            Guid customerId = Guid.NewGuid();
            _mockRentalService.Setup(service => service.GetTotalCostByCustomerId(customerId)).Throws(new CustomerNotFoundException("Customer does not exist"));

            // Act
            ActionResult<decimal> result = _controller.GetTotalCost(customerId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
            NotFoundObjectResult notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Customer does not exist", notFoundResult.Value);
        }
    }
}
