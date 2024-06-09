using Assignment_3.Entities;
using Assignment_3.Exceptions;
using Assignment_3.Repository.IRepository;
using Assignment_3.Services.IServices;
using Assignment_3.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentThreeTests.Services
{
    public class RentalServiceTests
    {
        private readonly Mock<IMovieService> _mockMovieService;
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly Mock<IRentalRepository> _mockRentalRepository;
        private readonly IRentalService _rentalService;

        public RentalServiceTests()
        {
            _mockMovieService = new Mock<IMovieService>();
            _mockCustomerService = new Mock<ICustomerService>();
            _mockRentalRepository = new Mock<IRentalRepository>();
            _rentalService = new RentalService(_mockMovieService.Object, _mockCustomerService.Object, _mockRentalRepository.Object);
        }

        [Fact]
        public void RentMovieById_MovieNotFound_ThrowsMovieNotFoundException()
        {
            // Arrange
            Guid movieId = Guid.NewGuid();
            Guid customerId = Guid.NewGuid();
            _mockMovieService.Setup(service => service.GetMovieById(movieId)).Returns((Movie)null);

            // Act & Assert
            Assert.Throws<MovieNotFoundException>(() => _rentalService.RentMovieById(movieId, customerId));
        }

        [Fact]
        public void RentMovieById_CustomerNotFound_ThrowsCustomerNotFoundException()
        {
            // Arrange
            Guid movieId = Guid.NewGuid();
            Guid customerId = Guid.NewGuid();
            Movie movie = new Movie("Test Movie", "Test Director", "Test Genre", 9.99m);
            _mockMovieService.Setup(service => service.GetMovieById(movieId)).Returns(movie);
            _mockCustomerService.Setup(service => service.GetCustomerById(customerId)).Returns((Customer)null);

            // Act & Assert
            Assert.Throws<CustomerNotFoundException>(() => _rentalService.RentMovieById(movieId, customerId));
        }

        [Fact]
        public void RentMovieById_RentalExists_ThrowsRentalAlreadyExistsException()
        {
            // Arrange
            Guid movieId = Guid.NewGuid();
            Guid customerId = Guid.NewGuid();
            Movie movie = new Movie("Test Movie", "Test Director", "Test Genre", 9.99m);
            Customer customer = new Customer("testuser", "Test User", "test@example.com");
            _mockMovieService.Setup(service => service.GetMovieById(movieId)).Returns(movie);
            _mockCustomerService.Setup(service => service.GetCustomerById(customerId)).Returns(customer);
            _mockRentalRepository.Setup(repo => repo.RentalExists(movieId, customerId)).Returns(true);

            // Act & Assert
            Assert.Throws<RentalAlreadyExistsException>(() => _rentalService.RentMovieById(movieId, customerId));
        }

        [Fact]
        public void RentMovieById_ValidRental_ReturnsRentalId()
        {
            // Arrange
            Guid movieId = Guid.NewGuid();
            Guid customerId = Guid.NewGuid();
            Movie movie = new Movie("Test Movie", "Test Director", "Test Genre", 9.99m);
            Customer customer = new Customer("testuser", "Test User", "test@example.com");
            Rental rental = new Rental(movieId, customerId);
            _mockMovieService.Setup(service => service.GetMovieById(movieId)).Returns(movie);
            _mockCustomerService.Setup(service => service.GetCustomerById(customerId)).Returns(customer);
            _mockRentalRepository.Setup(repo => repo.RentalExists(movieId, customerId)).Returns(false);
            _mockRentalRepository.Setup(repo => repo.AddRental(It.IsAny<Rental>())).Returns(rental.RentalId);

            // Act
            Guid result = _rentalService.RentMovieById(movieId, customerId);

            // Assert
            Assert.Equal(rental.RentalId, result);
        }

        [Fact]
        public void RentMovieByMovieTitleAndUsername_MovieNotFound_ThrowsMovieNotFoundException()
        {
            // Arrange
            string movieTitle = "Test Movie";
            string username = "testuser";
            _mockMovieService.Setup(service => service.GetMovieByTitle(movieTitle)).Returns((Movie)null);

            // Act & Assert
            Assert.Throws<MovieNotFoundException>(() => _rentalService.RentMovieByMovieTitleAndUsername(movieTitle, username));
        }

        [Fact]
        public void RentMovieByMovieTitleAndUsername_CustomerNotFound_ThrowsCustomerNotFoundException()
        {
            // Arrange
            string movieTitle = "Test Movie";
            string username = "testuser";
            Movie movie = new Movie(movieTitle, "Test Director", "Test Genre", 9.99m);
            _mockMovieService.Setup(service => service.GetMovieByTitle(movieTitle)).Returns(movie);
            _mockCustomerService.Setup(service => service.GetCustomerByUsername(username)).Returns((Customer)null);

            // Act & Assert
            Assert.Throws<CustomerNotFoundException>(() => _rentalService.RentMovieByMovieTitleAndUsername(movieTitle, username));
        }

        [Fact]
        public void RentMovieByMovieTitleAndUsername_RentalExists_ThrowsRentalAlreadyExistsException()
        {
            // Arrange
            string movieTitle = "Test Movie";
            string username = "testuser";
            Movie movie = new Movie(movieTitle, "Test Director", "Test Genre", 9.99m);
            Customer customer = new Customer(username, "Test User", "test@example.com");
            _mockMovieService.Setup(service => service.GetMovieByTitle(movieTitle)).Returns(movie);
            _mockCustomerService.Setup(service => service.GetCustomerByUsername(username)).Returns(customer);
            _mockRentalRepository.Setup(repo => repo.RentalExists(movie.MovieId, customer.CustomerId)).Returns(true);

            // Act & Assert
            Assert.Throws<RentalAlreadyExistsException>(() => _rentalService.RentMovieByMovieTitleAndUsername(movieTitle, username));
        }

        [Fact]
        public void GetCustomersByMovieId_ValidMovieId_ReturnsCustomerUsernames()
        {
            // Arrange
            Guid movieId = Guid.NewGuid();
            Movie movie = new Movie("Test Movie", "Test Director", "Test Genre", 9.99m);
            List<Guid> customerIdList = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            List<string> expectedUsernames = new List<string> { "user1", "user2", "user3" };

            _mockMovieService.Setup(service => service.GetMovieById(movieId)).Returns(movie);
            _mockRentalRepository.Setup(repo => repo.GetCustomerIdsByMovieId(movieId)).Returns(customerIdList);
            _mockRentalRepository.Setup(repo => repo.GetCustomerUsernamesByCustomerIds(customerIdList)).Returns(expectedUsernames);

            // Act
            List<string> result = _rentalService.GetCustomersByMovieId(movieId);

            // Assert
            Assert.Equal(expectedUsernames, result);
        }

        [Fact]
        public void GetCustomersByMovieId_InvalidMovieId_ThrowsMovieNotFoundException()
        {
            // Arrange
            Guid movieId = Guid.NewGuid();
            _mockMovieService.Setup(service => service.GetMovieById(movieId)).Returns<Movie>(null);

            // Act & Assert
            Assert.Throws<MovieNotFoundException>(() => _rentalService.GetCustomersByMovieId(movieId));
        }

        [Fact]
        public void GetCustomerIdsByMovieId_ValidMovieId_ReturnsCustomerIds()
        {
            // Arrange
            Guid movieId = Guid.NewGuid();
            List<Guid> customerIdList = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            _mockRentalRepository.Setup(repo => repo.GetCustomerIdsByMovieId(movieId)).Returns(customerIdList);

            // Act
            List<Guid> result = _rentalService.GetCustomerIdsByMovieId(movieId);

            // Assert
            Assert.Equal(customerIdList, result);
        }

        [Fact]
        public void GetMoviesByCustomerId_ValidCustomerId_ReturnsMovieTitles()
        {
            // Arrange
            Guid customerId = Guid.NewGuid();
            Customer customer = new Customer("testuser", "Test User", "test@example.com");
            List<Guid> rentalMovieIdList = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            List<string> expectedTitles = new List<string> { "Movie 1", "Movie 2", "Movie 3" };

            _mockCustomerService.Setup(service => service.GetCustomerById(customerId)).Returns(customer);
            _mockRentalRepository.Setup(repo => repo.GetMovieIdsByCustomerId(customerId)).Returns(rentalMovieIdList);
            _mockRentalRepository.Setup(repo => repo.GetMovieTitlesByMovieIds(rentalMovieIdList)).Returns(expectedTitles);

            // Act
            List<string> result = _rentalService.GetMoviesByCustomerId(customerId);

            // Assert
            Assert.Equal(expectedTitles, result);
        }

        [Fact]
        public void GetMoviesByCustomerId_InvalidCustomerId_ThrowsCustomerNotFoundException()
        {
            // Arrange
            Guid customerId = Guid.NewGuid();
            _mockCustomerService.Setup(service => service.GetCustomerById(customerId)).Returns<Customer>(null);

            // Act & Assert
            Assert.Throws<CustomerNotFoundException>(() => _rentalService.GetMoviesByCustomerId(customerId));
        }

        [Fact]
        public void GetTotalCostByCustomerId_ValidCustomerId_ReturnsTotalCost()
        {
            // Arrange
            Guid customerId = Guid.NewGuid();
            Customer customer = new Customer("testuser", "Test User", "test@example.com");
            decimal expectedTotalCost = 100.50m;

            _mockCustomerService.Setup(service => service.GetCustomerById(customerId)).Returns(customer);
            _mockRentalRepository.Setup(repo => repo.CalculateTotalRentalCost(customerId)).Returns(expectedTotalCost);

            // Act
            decimal result = _rentalService.GetTotalCostByCustomerId(customerId);

            // Assert
            Assert.Equal(expectedTotalCost, result);
        }

        [Fact]
        public void GetTotalCostByCustomerId_InvalidCustomerId_ThrowsCustomerNotFoundException()
        {
            // Arrange
            Guid customerId = Guid.NewGuid();
            _mockCustomerService.Setup(service => service.GetCustomerById(customerId)).Returns<Customer>(null);

            // Act & Assert
            Assert.Throws<CustomerNotFoundException>(() => _rentalService.GetTotalCostByCustomerId(customerId));
        }

    }
}
