using Assignment_3.Entities;
using System.ComponentModel.DataAnnotations;

namespace Assignment_3.Dtos
{
    public class CreateMovieRequestDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, ErrorMessage = "Title length can't be more than 50 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Director is required.")]
        [StringLength(50, ErrorMessage = "Director length can't be more than 50 characters.")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Genre is required.")]
        [StringLength(50, ErrorMessage = "Genre length can't be more than 50 characters.")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999999.99.")]
        public decimal Price { get; set; }
    }
}
