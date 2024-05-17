using Assignment_3.DataAccess;
using Assignment_3.Entities;
using Assignment_3.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Assignment_3.Services
{
    public class MovieService
    {
        private readonly MovieRentalDbContext _dbContext;

        public MovieService(MovieRentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid AddMovie(Movie movie)
        {
            if (_dbContext.Movies.Any(m => m.Title == movie.Title))
            {
                throw new MovieAlreadyExistsException($"Movie with title '{movie.Title}' already exists.");
            }
            try
            {
                _dbContext.Movies.Add(movie);
                _dbContext.SaveChanges();

                return movie.MovieId;
            }
            catch (Exception ex)
            {
                throw new FailedToAddMovieException("Failed to add movie. An error occurred.", ex);
            }
        }

        public List<Movie> GetAllMovies()
        {
            return _dbContext.Movies.ToList();
        }

        public Movie GetMovieById(Guid id)
        {
            Movie? movie = _dbContext.Movies.Find(id);
            if (movie == null)
            {
                throw new MovieNotFoundException("Movie not found.");
            }
            return movie;
        }

        public Movie GetMovieByTitle(string title)
        {
            Movie? movie = _dbContext.Movies.FirstOrDefault(m => m.Title == title);
            if (movie == null)
            {
                throw new MovieNotFoundException("Movie not found.");
            }
            return movie;
        }
    }
}
