using Assignment_3.Entities;

namespace Assignment_3.Services.IServices
{
    public interface IMovieService
    {
        Guid AddMovie(Movie movie);
        List<Movie> GetAllMovies();
        Movie GetMovieById(Guid id);
        Movie GetMovieByTitle(string title);
    }
}
