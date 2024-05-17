using System.ComponentModel.DataAnnotations;

namespace Assignment_3.Dtos
{
    public class RentMovieByIdRequestDto
    {
        [Required(ErrorMessage = "Movie Id is required.")]
        public Guid MovieId { get; set; }

        [Required(ErrorMessage = "Customer Id is required.")]
        public Guid CustomerId { get; set; }
    }
}
