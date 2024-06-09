using System.ComponentModel.DataAnnotations;

namespace Assignment_3.Dtos
{
    public class RentMovieByMovieNameAndUserNameRequestViewModel
    {
        [Required(ErrorMessage = "Movie title is required")]
        [StringLength(50, ErrorMessage = "Title length can't be more than 50 characters.")]
        public string MovieTitle { get; set; }

        [Required(ErrorMessage = "Customer username is required")]
        [StringLength(50, ErrorMessage = "Username length can't be more than 50.")]
        public string CustomerUsername { get; set; }
    }
}
