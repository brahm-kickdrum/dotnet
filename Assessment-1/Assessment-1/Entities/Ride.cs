using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment_1.Entities
{
    public class Ride
    {
        [Required]
        public Guid RideId { get; set; }
        [Required]
        public Guid DriverId { get; set; }
        [Required]
        public Guid RiderId { get; set; }
        [Required]
        public DateTime RideRequestDT { get; set; }
        [Required]
        public DateTime RideStartDT { get; set; }
        [Required]
        public DateTime RideEndDT { get; set; }
        [Required]
        public string StartLocation {  get; set; }
        [Required]
        public string EndLocation { get; set; }
        [Required]
        public string VehicleId { get; set; }
        [Required]
        public Decimal Fare { get; set; }

        [ForeignKey("RiderId")]
        public Rider Rider { get; set; }

        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }

        public Ride(
            Guid driverId,
            Guid riderId,
            DateTime rideRequestDT,
            DateTime rideStartDT,
            DateTime rideEndDT,
            string startLocation,
            string endLocation,
            string vehicleId,
            decimal fare)
        {
            RideId = Guid.NewGuid();
            DriverId = driverId;
            RiderId = riderId;
            RideRequestDT = rideRequestDT;
            RideStartDT = rideStartDT;
            RideEndDT = rideEndDT;
            StartLocation = startLocation;
            EndLocation = endLocation;
            VehicleId = vehicleId;
            Fare = fare;
        }
    }
}
