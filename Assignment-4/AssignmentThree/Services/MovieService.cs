using Assignment_3.DataAccess;
using Assignment_3.Entities;
using Assignment_3.Exceptions;
using Assignment_3.Repository;
using Assignment_3.Repository.IRepository;
using Assignment_3.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Assignment_3.Services
{
    public class MovieService : IMovieService
    {
        private IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public Guid AddMovie(Movie movie)
        {
            if (_movieRepository.MovieExistsByTitle(movie.Title))
            {
                throw new MovieAlreadyExistsException($"Movie with title '{movie.Title}' already exists.");
            }
            try
            {
                return _movieRepository.AddMovie(movie);
            }
            catch (Exception ex)
            {
                throw new FailedToAddMovieException("Failed to add movie. An error occurred.", ex);
            }
        }

        public List<Movie> GetAllMovies()
        {
            return _movieRepository.GetAllMovies();
        }

        public Movie GetMovieById(Guid id)
        {
            Movie? movie = _movieRepository.GetMovieById(id);

            if (movie == null)
            {
                throw new MovieNotFoundException("Movie not found.");
            }

            return movie;
        }

        public Movie GetMovieByTitle(string title)
        {
            Movie? movie = _movieRepository.GetMovieByTitle(title);

            if (movie == null)
            {
                throw new MovieNotFoundException("Movie not found.");
            }

            return movie;
        }
    }
}
