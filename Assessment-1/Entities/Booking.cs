using Assessment_1.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment_1.Entities
{
    public class Booking
    {
        [Key]
        public Guid BookingId { get; set; }

        [Required]
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
        public TripStatus RideStatus { get; set; }

        [ForeignKey("RiderId")]
        public User Rider { get; set; }

        [ForeignKey("DriverId")]
        public User Driver { get; set; }

        public Booking() { }

        public Booking(Guid tripId, Guid riderId, Guid driverId, string pickupLocation, string dropLocation, DateTime rideRequestDateTime, DateTime? startDateTime, DateTime? endDateTime, decimal fare, TripStatus rideStatus)
        {
            BookingId = new Guid();
            TripId = tripId;
            RiderId = riderId;
            DriverId = driverId;
            PickupLocation = pickupLocation;
            DropLocation = dropLocation;
            RideRequestDateTime = rideRequestDateTime;
            Fare = fare;
            RideStatus = rideStatus;
        }

    }
}
