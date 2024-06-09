using Assignment_3.DataAccess;
using Assignment_3.Entities;
using Assignment_3.Repository.IRepository;

namespace Assignment_3.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieRentalDbContext _dbContext;

        public MovieRepository(MovieRentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid AddMovie(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();

            return movie.MovieId;
        }

        public bool MovieExistsByTitle(string movieTitle)
        {
            return _dbContext.Movies.Any(m => m.Title == movieTitle);
        }

        public List<Movie> GetAllMovies()
        {
            return _dbContext.Movies.ToList();
        }

        public Movie? GetMovieById(Guid id)
        {
            return _dbContext.Movies.Find(id);
        }

        public Movie? GetMovieByTitle(string title)
        {
            return _dbContext.Movies.FirstOrDefault(m => m.Title == title);
        }
    }
}
