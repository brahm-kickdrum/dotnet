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
            CreateMovieRequestViewModel movieRequestViewModel = new CreateMovieRequestViewModel
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
            ActionResult<MovieIdResponseViewModel> result = _controller.Post(movieRequestViewModel);

            // Assert
            ActionResult<MovieIdResponseViewModel> actionResult = Assert.IsType<ActionResult<MovieIdResponseViewModel>>(result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            MovieIdResponseViewModel responseViewModel = Assert.IsType<MovieIdResponseViewModel>(okResult.Value);
            Assert.Equal(movieId, responseViewModel.MovieId);
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
            ActionResult<MovieListResponseViewModel> result = _controller.GetAllMovies();

            // Assert
            ActionResult<MovieListResponseViewModel> actionResult = Assert.IsType<ActionResult<MovieListResponseViewModel>>(result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            MovieListResponseViewModel responseViewModel = Assert.IsType<MovieListResponseViewModel>(okResult.Value);
            Assert.Equal(movies, responseViewModel.MovieList);
        }

        [Fact]
        public void GetMovieById_ReturnsOkResult_WithMovieViewModel()
        {
            // Arrange
            Movie movieEntity = new Movie("Test Movie", "Test Director", "Test Genre", 100);
            Guid movieId = movieEntity.MovieId;
            MovieResponseViewModel movieResponseViewModel = new MovieResponseViewModel(movieId, "Test Movie", "Test Director", "Test Genre", 100);
            _mockMovieService.Setup(service => service.GetMovieById(movieId)).Returns(movieEntity);

            // Act
            ActionResult<MovieResponseViewModel> result = _controller.GetMovieById(movieId);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            MovieResponseViewModel model = Assert.IsType<MovieResponseViewModel>(okResult.Value);
            Assert.Equal(movieResponseViewModel.MovieId, model.MovieId);
            Assert.Equal(movieResponseViewModel.Title, model.Title);
            Assert.Equal(movieResponseViewModel.Director, model.Director);
            Assert.Equal(movieResponseViewModel.Genre, model.Genre);
            Assert.Equal(movieResponseViewModel.Price, model.Price);
        }

        [Fact]
        public void GetMovieByTitle_ReturnsOkResult_WithMovieViewModel()
        {
            // Arrange
            string movieTitle = "Test Movie";
            Movie movie = new Movie(movieTitle, "Test Director", "Test Genre", 100);
            Guid movieId = movie.MovieId;
            MovieResponseViewModel movieResponseViewModel = new MovieResponseViewModel(movieId, movieTitle, "Test Director", "Test Genre", 100);
            _mockMovieService.Setup(service => service.GetMovieByTitle(movieTitle)).Returns(movie);

            // Act
            ActionResult<MovieResponseViewModel> result = _controller.GetMovieByTitle(movieTitle);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            MovieResponseViewModel model = Assert.IsType<MovieResponseViewModel>(okResult.Value);
            Assert.Equal(movie.MovieId, model.MovieId);
            Assert.Equal(movie.Title, model.Title);
            Assert.Equal(movie.Director, model.Director);
            Assert.Equal(movie.Genre, model.Genre);
            Assert.Equal(movie.Price, model.Price);
        }

    }
}
