using Assignment_3.Entities;
using Microsoft.AspNetCore.Mvc;
using Assignment_3.Dtos;
using Assignment_3.Services;
using Assignment_3.Mappers;
using Assignment_3.Exceptions;

namespace Assignment_3.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public ActionResult<MovieIdResponseDto> Post([FromBody] CreateMovieRequestDto createMovieRequestDto)
        {
            try
            { 
                Guid movieId = _movieService.AddMovie(MovieMapper.CreateMovieFromDto(createMovieRequestDto));
                return Ok(MovieMapper.CreateMovieIdResponseDtoFromMovieId(movieId));
            }
            catch (FailedToAddMovieException ex) 
            {
                return StatusCode(500, ex.Message);
            }
            catch (MovieAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Movie>> GetAllMovies()
        {
            List<Movie> movieList = _movieService.GetAllMovies();
            return Ok(movieList);
        }

        [HttpGet("id/{id}")]
        public ActionResult<Movie> GetMovieById(Guid id)
        {
            try
            {
                Movie movie = _movieService.GetMovieById(id);
                return Ok(movie);
            }
            catch (MovieNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("title/{title}")]
        public ActionResult<Movie> GetMovieByTitle(string title)
        {
            try
            {
                Movie movie = _movieService.GetMovieByTitle(title);
                return Ok(movie);
            }
            catch (MovieNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
