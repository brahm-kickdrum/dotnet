using System.ComponentModel.DataAnnotations;

namespace Assessment_1.Models
{
    public class EndTripRequest
    {
        [Required]
        public Guid TripId { get; set; }
    }
}
