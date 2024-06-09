using Assignment_3.Entities;
using Assignment_3.Exceptions;
using Assignment_3.Repository.IRepository;
using Assignment_3.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentThreeTests.Services
{
    public class MovieServiceTests
    {
        private readonly Mock<IMovieRepository> _mockMovieRepository;
        private readonly MovieService _movieService;

        public MovieServiceTests()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
            _movieService = new MovieService(_mockMovieRepository.Object);
        }

        [Fact]
        public void AddMovie_MovieExists_ThrowsMovieAlreadyExistsException()
        {
            // Arrange
            Movie movie = new Movie("existingTitle", "Test Director", "Test Genre", 9.99m);
            _mockMovieRepository.Setup(repo => repo.MovieExistsByTitle(movie.Title)).Returns(true);

            // Act & Assert
            Assert.Throws<MovieAlreadyExistsException>(() => _movieService.AddMovie(movie));
        }

        [Fact]
        public void AddMovie_ValidMovie_ReturnsMovieId()
        {
            // Arrange
            Movie movie = new Movie("newTitle", "Test Director", "Test Genre", 9.99m);
            Guid movieId = movie.MovieId;
            _mockMovieRepository.Setup(repo => repo.MovieExistsByTitle(movie.Title)).Returns(false);
            _mockMovieRepository.Setup(repo => repo.AddMovie(movie)).Returns(movieId);

            // Act
            Guid result = _movieService.AddMovie(movie);

            // Assert
            Assert.Equal(movieId, result);
        }

        [Fact]
        public void GetMovieById_MovieNotFound_ThrowsMovieNotFoundException()
        {
            // Arrange
            Guid movieId = Guid.NewGuid();
            _mockMovieRepository.Setup(repo => repo.GetMovieById(movieId)).Returns((Movie)null);

            // Act & Assert
            Assert.Throws<MovieNotFoundException>(() => _movieService.GetMovieById(movieId));
        }

        [Fact]
        public void GetMovieById_MovieFound_ReturnsMovie()
        {
            // Arrange
            Guid movieId = Guid.NewGuid();
            Movie movie = new Movie("testTitle", "Test Director", "Test Genre", 9.99m) { MovieId = movieId };
            _mockMovieRepository.Setup(repo => repo.GetMovieById(movieId)).Returns(movie);

            // Act
            Movie result = _movieService.GetMovieById(movieId);

            // Assert
            Assert.Equal(movie, result);
        }

        [Fact]
        public void GetMovieByTitle_MovieNotFound_ThrowsMovieNotFoundException()
        {
            // Arrange
            string title = "nonexistentTitle";
            _mockMovieRepository.Setup(repo => repo.GetMovieByTitle(title)).Returns((Movie)null);

            // Act & Assert
            Assert.Throws<MovieNotFoundException>(() => _movieService.GetMovieByTitle(title));
        }

        [Fact]
        public void GetMovieByTitle_MovieFound_ReturnsMovie()
        {
            // Arrange
            string title = "existingTitle";
            Movie movie = new Movie(title, "Test Director", "Test Genre", 9.99m);
            _mockMovieRepository.Setup(repo => repo.GetMovieByTitle(title)).Returns(movie);

            // Act
            Movie result = _movieService.GetMovieByTitle(title);

            // Assert
            Assert.Equal(movie, result);
        }

        [Fact]
        public void GetAllMovies_ReturnsListOfMovies()
        {
            // Arrange
            List<Movie> movies = new List<Movie>
            {
                new Movie("title1", "Director1", "Genre1", 9.99m),
                new Movie("title2", "Director2", "Genre2", 19.99m)
            };
            _mockMovieRepository.Setup(repo => repo.GetAllMovies()).Returns(movies);

            // Act
            List<Movie> result = _movieService.GetAllMovies();

            // Assert
            Assert.Equal(movies, result);
        }
    }
}
