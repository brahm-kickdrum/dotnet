using System.ComponentModel.DataAnnotations;

namespace Assessment_1.Models
{
    public class RatingRequest
    {
        [Required(ErrorMessage = "Trip ID is required.")]
        public Guid TripId { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int Rating { get; set; }
    }
}
