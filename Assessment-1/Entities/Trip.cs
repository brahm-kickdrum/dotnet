using Assessment_1.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment_1.Entities
{
    public class Trip
    {
        [Key]
        public Guid TripId { get; set; }

        [Required]
        public Guid RiderId { get; set; }

        [Required]
        public Guid DriverId { get; set; }

        [Required]
        public string PickupLocation { get; set; }

        [Required]
        public string DropLocation { get; set; }

        [Required]
        public DateTime RideRequestDateTime { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        [Required]
        public decimal Fare { get; set; }

        [Required]
        public string OTP { get; set; }

        [Required]
        public TripStatus RideStatus { get; set; }

        [ForeignKey("RiderId")]
        public User Rider { get; set; }

        [ForeignKey("DriverId")]
        public User Driver { get; set; }

        public Trip() { }

        public Trip(Guid riderId, Guid driverId, string pickupLocation, string dropLocation,string otp, TripStatus rideStatus)
        {
            TripId = new Guid(); 
            RiderId = riderId;
            DriverId = driverId;
            PickupLocation = pickupLocation;
            DropLocation = dropLocation;
            OTP = otp;
            RideRequestDateTime = DateTime.UtcNow;
            RideStatus = rideStatus;
        }
    }
}
