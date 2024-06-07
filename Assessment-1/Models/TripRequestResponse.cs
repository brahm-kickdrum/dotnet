using Assessment_1.Enums;
using System.ComponentModel.DataAnnotations;

namespace Assessment_1.Models
{
    public class TripRequestResponse
    {
        [Required(ErrorMessage = "Trip Id is required.")]
        public Guid TripId { get; set; }

        [Required(ErrorMessage = "Plate number is required.")]
        [StringLength(15, ErrorMessage = "Plate number cannot be longer than 15 characters.")]
        [RegularExpression("^[A-Z0-9-]+$", ErrorMessage = "Plate number can only contain uppercase letters, numbers, and hyphens.")]
        public string PlateNumber { get; set; }

        [Required(ErrorMessage = "Vehicle type is required.")]
        public VehicleType VehicleType { get; set; }

        [Required(ErrorMessage = "License number is required.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "License number must be exactly 16 digits.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "License number must contain only digits.")]
        public string LicenseNumber { get; set; }

        [Required(ErrorMessage = "OTP is required.")]
        public string OTP { get; set; }

        [Required(ErrorMessage = "Ride status is required.")]
        public TripStatus RideStatus { get; set; }

        public TripRequestResponse(Guid tripId, string plateNumber, VehicleType vehicleType, string licenseNumber, string otp, TripStatus rideStatus)
        {
            TripId = tripId;
            PlateNumber = plateNumber;
            VehicleType = vehicleType;
            LicenseNumber = licenseNumber;
            OTP = otp;
            RideStatus = rideStatus;
        }
    }
}
