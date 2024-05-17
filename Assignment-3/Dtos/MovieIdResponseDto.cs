namespace Assignment_3.Dtos
{
    public class MovieIdResponseDto
    {
        Guid MovieId { get; set; }

        public MovieIdResponseDto(Guid id)
        {
            MovieId = id;
        }
    }
}
