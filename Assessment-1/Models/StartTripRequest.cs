using System.ComponentModel.DataAnnotations;

namespace Assessment_1.Models
{
    public class StartTripRequest
    {
        [Required]
        public Guid TripId { get; set; }

        [Required]
        public string OTP { get; set; }
    }
}
