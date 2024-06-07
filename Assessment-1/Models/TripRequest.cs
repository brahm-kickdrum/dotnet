using Assessment_1.Entities;
using Assessment_1.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assessment_1.Models
{
    public class TripRequest
    {
        [Required(ErrorMessage = "Pickup location is required.")]
        public string PickupLocation { get; set; }

        [Required(ErrorMessage = "Drop location is required.")]
        public string DropLocation { get; set; }

        [Required(ErrorMessage = "Ride type is required.")]
        public VehicleType RideType { get; set; }
    }
}
