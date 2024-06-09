using Assignment_3.Entities;

namespace Assignment_3.Repository.IRepository
{
    public interface IMovieRepository
    {
        Guid AddMovie(Movie movie);

        bool MovieExistsByTitle(string movieTitle);

        List<Movie> GetAllMovies();

        Movie? GetMovieById(Guid id);

        Movie? GetMovieByTitle(string title);
    }
}
