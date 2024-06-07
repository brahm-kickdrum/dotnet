using System.ComponentModel.DataAnnotations;

namespace Assessment_1.Models
{
    public class CancelTripRequest
    {
        [Required]
        public Guid TripId { get; set; }
    }
}
