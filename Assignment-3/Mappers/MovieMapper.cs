using Assignment_3.Dtos;
using Assignment_3.Entities;

namespace Assignment_3.Mappers
{
    public static class MovieMapper
    {
        public static Movie CreateMovieFromViewModel(CreateMovieRequestViewModel viewModel)
        {
            return new Movie(viewModel.Title, viewModel.Director, viewModel.Genre, viewModel.Price);
        }

        public static MovieIdResponseViewModel CreateMovieIdResponseViewModelFromMovieId(Guid movieId)
        {
            return new MovieIdResponseViewModel(movieId);
        }

        public static MovieListResponseViewModel CreateMovieListResponseViewModelFromMovieList(List<Movie> movieList)
        {
            return new MovieListResponseViewModel(movieList);
        }

        public static MovieResponseViewModel CreateMovieResponseViewModelFromMovie(Movie movie)
        {
            return new MovieResponseViewModel(movie.MovieId, movie.Title, movie.Director, movie.Genre, movie.Price);
        }

    }
}
