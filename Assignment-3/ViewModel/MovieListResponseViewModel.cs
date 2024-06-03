using Assignment_3.Entities;

namespace Assignment_3.Dtos
{
    public class MovieListResponseViewModel
    {
        public List<Movie> MovieList {  get; set; }

        public MovieListResponseViewModel(List<Movie> movieList)
        {
            MovieList = movieList;
        }
    }
}
