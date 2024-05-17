using Assignment_3.Dtos;
using Assignment_3.Entities;

namespace Assignment_3.Mappers
{
    public static class MovieMapper
    {
        public static Movie CreateMovieFromDto(CreateMovieRequestDto dto)
        {
            return new Movie(dto.Title, dto.Director, dto.Genre, dto.Price);
        }

        public static MovieIdResponseDto CreateMovieIdResponseDtoFromMovieId(Guid movieId)
        {
            return new MovieIdResponseDto(movieId);
        }  
    }
}
