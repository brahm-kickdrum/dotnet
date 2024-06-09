using Assignment_3.Dtos;
using Assignment_3.Entities;
using Assignment_3.Exceptions;
using Assignment_3.Mappers;
using Assignment_3.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_3.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public ActionResult<MovieIdResponseViewModel> Post([FromBody] CreateMovieRequestViewModel createMovieRequestViewModel)
        {
            Guid movieId = _movieService.AddMovie(MovieMapper.CreateMovieFromViewModel(createMovieRequestViewModel));
            return Ok(MovieMapper.CreateMovieIdResponseViewModelFromMovieId(movieId));
        }

        [HttpGet]
        public ActionResult<MovieListResponseViewModel> GetAllMovies()
        {
            List<Movie> movieList = _movieService.GetAllMovies();
            return Ok(MovieMapper.CreateMovieListResponseViewModelFromMovieList(movieList));
        }

        [HttpGet("id/{id}")]
        public ActionResult<MovieResponseViewModel> GetMovieById(Guid id)
        {
            Movie movie = _movieService.GetMovieById(id);
            MovieResponseViewModel movieViewModel = MovieMapper.CreateMovieResponseViewModelFromMovie(movie); 
            return Ok(movieViewModel);
        }

        [HttpGet("title/{title}")]
        public ActionResult<MovieResponseViewModel> GetMovieByTitle(string title)
        {
            Movie movie = _movieService.GetMovieByTitle(title);
            return Ok(MovieMapper.CreateMovieResponseViewModelFromMovie(movie));
        }
    }
}
