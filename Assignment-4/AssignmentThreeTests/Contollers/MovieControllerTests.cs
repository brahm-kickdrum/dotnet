using Assignment_3.Controllers;
using Assignment_3.Dtos;
using Assignment_3.Entities;
using Assignment_3.Exceptions;
using Assignment_3.Services;
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
    public class MovieControllerTests
    {
        private readonly Mock<IMovieService> _mockMovieService;
        private readonly MovieController _controller;

        public MovieControllerTests()
        {
            _mockMovieService = new Mock<IMovieService>();
            _controller = new MovieController(_mockMovieService.Object);
        }

        [Fact]
        public void Post_ReturnsOkResult_WhenMovieIsAddedSuccessfully()
        {
            // Arrange
            CreateMovieRequestDto movieRequestDto = new CreateMovieRequestDto
            {
                Title = "Test Movie",
                Director = "Test Director",
                Genre = "Test Genre",
                Price = 100
            };
            Guid movieId = Guid.NewGuid();

            _mockMovieService.Setup(service => service.AddMovie(It.IsAny<Movie>()))
                .Returns(movieId);

            // Act
            ActionResult<MovieIdResponseDto> result = _controller.Post(movieRequestDto);

            // Assert
            ActionResult<MovieIdResponseDto> actionResult = Assert.IsType<ActionResult<MovieIdResponseDto>>(result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            MovieIdResponseDto responseDto = Assert.IsType<MovieIdResponseDto>(okResult.Value);
            Assert.Equal(movieId, responseDto.MovieId);
        }

        [Fact]
        public void Post_ReturnsBadRequest_WhenMovieAlreadyExists()
        {
            // Arrange
            CreateMovieRequestDto movieRequestDto = new CreateMovieRequestDto
            {
                Title = "Existing Movie",
                Director = "Existing Director",
                Genre = "Existing Genre",
                Price = 100
            };

            _mockMovieService.Setup(service => service.AddMovie(It.IsAny<Movie>()))
                .Throws(new MovieAlreadyExistsException("Movie already exists"));

            // Act
            ActionResult<MovieIdResponseDto> result = _controller.Post(movieRequestDto);

            // Assert
            ActionResult<MovieIdResponseDto> actionResult = Assert.IsType<ActionResult<MovieIdResponseDto>>(result);
            BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Movie already exists", badRequestResult.Value);
        }

        [Fact]
        public void Post_ReturnsInternalServerError_WhenFailedToAddMovie()
        {
            // Arrange
            CreateMovieRequestDto movieRequestDto = new CreateMovieRequestDto
            {
                Title = "New Movie",
                Director = "New Director",
                Genre = "New Genre",
                Price = 100
            };

            _mockMovieService.Setup(service => service.AddMovie(It.IsAny<Movie>()))
                .Throws(new FailedToAddMovieException("Failed to add movie"));

            // Act
            ActionResult<MovieIdResponseDto> result = _controller.Post(movieRequestDto);

            // Assert
            ActionResult<MovieIdResponseDto> actionResult = Assert.IsType<ActionResult<MovieIdResponseDto>>(result);
            ObjectResult statusCodeResult = Assert.IsType<ObjectResult>(actionResult.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Failed to add movie", statusCodeResult.Value);
        }

        [Fact]
        public void GetAllMovies_ReturnsOkResult_WithMovieList()
        {
            // Arrange
            List<Movie> movies = new List<Movie>
            {
                new Movie("Movie 1", "Director 1", "Genre 1", 100),
                new Movie("Movie 2", "Director 2", "Genre 2", 200)
            };

            _mockMovieService.Setup(service => service.GetAllMovies())
                .Returns(movies);

            // Act
            ActionResult<MovieListResponseDto> result = _controller.GetAllMovies();

            // Assert
            ActionResult<MovieListResponseDto> actionResult = Assert.IsType<ActionResult<MovieListResponseDto>>(result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            MovieListResponseDto responseDto = Assert.IsType<MovieListResponseDto>(okResult.Value);
            Assert.Equal(movies, responseDto.MovieList);
        }

        [Fact]
        public void GetMovieById_ReturnsOkResult_WithMovieDto()
        {
            // Arrange
            Movie movieEntity = new Movie("Test Movie", "Test Director", "Test Genre", 100);
            Guid movieId = movieEntity.MovieId;
            MovieResponseDto movieResponseDto = new MovieResponseDto(movieId, "Test Movie", "Test Director", "Test Genre", 100);
            _mockMovieService.Setup(service => service.GetMovieById(movieId)).Returns(movieEntity);

            // Act
            ActionResult<MovieResponseDto> result = _controller.GetMovieById(movieId);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            MovieResponseDto model = Assert.IsType<MovieResponseDto>(okResult.Value);
            Assert.Equal(movieResponseDto.MovieId, model.MovieId);
            Assert.Equal(movieResponseDto.Title, model.Title);
            Assert.Equal(movieResponseDto.Director, model.Director);
            Assert.Equal(movieResponseDto.Genre, model.Genre);
            Assert.Equal(movieResponseDto.Price, model.Price);
        }

        [Fact]
        public void GetMovieById_ReturnsNotFound_WhenMovieNotFound()
        {
            // Arrange
            Guid movieId = Guid.NewGuid();
            _mockMovieService.Setup(service => service.GetMovieById(movieId)).Throws(new MovieNotFoundException("Movie not found"));

            // Act
            ActionResult<MovieResponseDto> result = _controller.GetMovieById(movieId);

            // Assert
            ActionResult<MovieResponseDto> actionResult = Assert.IsType<ActionResult<MovieResponseDto>>(result);
            NotFoundObjectResult notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("Movie not found", notFoundResult.Value);
        }

        [Fact]
        public void GetMovieByTitle_ReturnsOkResult_WithMovieDto()
        {
            // Arrange
            string movieTitle = "Test Movie";
            Movie movie = new Movie(movieTitle, "Test Director", "Test Genre", 100);
            Guid movieId = movie.MovieId;
            MovieResponseDto movieResponseDto = new MovieResponseDto(movieId, movieTitle, "Test Director", "Test Genre", 100);
            _mockMovieService.Setup(service => service.GetMovieByTitle(movieTitle)).Returns(movie);

            // Act
            ActionResult<MovieResponseDto> result = _controller.GetMovieByTitle(movieTitle);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            MovieResponseDto model = Assert.IsType<MovieResponseDto>(okResult.Value);
            Assert.Equal(movie.MovieId, model.MovieId);
            Assert.Equal(movie.Title, model.Title);
            Assert.Equal(movie.Director, model.Director);
            Assert.Equal(movie.Genre, model.Genre);
            Assert.Equal(movie.Price, model.Price);
        }

        [Fact]
        public void GetMovieByTitle_ReturnsNotFound_WhenMovieNotFound()
        {
            // Arrange
            string movieTitle = "Non-existent Movie";
            _mockMovieService.Setup(service => service.GetMovieByTitle(movieTitle)).Throws(new MovieNotFoundException("Movie not found"));

            // Act
            ActionResult<MovieResponseDto> result = _controller.GetMovieByTitle(movieTitle);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
            NotFoundObjectResult notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Movie not found", notFoundResult.Value);
        }
    }
}
