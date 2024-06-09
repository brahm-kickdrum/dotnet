using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assessment_1.Entities
{
    public class Rating
    {
        [Key]
        public Guid RatingId { get; set; }

        [Required]
        public Guid TripId { get; set; }

        [Range(0, 5)]
        public int? RiderRating { get; set; }

        [Range(0, 5)]
        public int? DriverRating { get; set; }

        [ForeignKey("TripId")] 
        public Booking Booking { get; set; }

        public Rating(Guid tripId)
        {
            RatingId = new Guid();
            TripId = tripId;
        }
    }
}
