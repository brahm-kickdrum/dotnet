using Assessment_1.Enums;
using Assessment_1.Validations;
using System.ComponentModel.DataAnnotations;

namespace Assessment_1.Models
{
    public class RiderTripDetailsResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string PlateNumber { get; set; }

        public VehicleType VehicleType { get; set; }

        public string LicenseNumber { get; set; }

        public TripStatus RideStatus { get; set; }

        public string? Otp {  get; set; }

        public RiderTripDetailsResponse(string firstName, string lastName, string phone, string plateNumber, VehicleType vehicleType, string licenseNumber, TripStatus rideStatus, string? otp)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            PlateNumber = plateNumber;
            VehicleType = vehicleType;
            LicenseNumber = licenseNumber;
            RideStatus = rideStatus;
            Otp = otp;
        }
    }
}
