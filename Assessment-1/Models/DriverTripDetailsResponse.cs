using Assessment_1.Enums;
using System.ComponentModel.DataAnnotations;

namespace Assessment_1.Models
{
    public class DriverTripDetailsResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public TripStatus RideStatus { get; set; }

        public string PickupLocation { get; set; }

        public string DropLocation { get; set; }

        public DriverTripDetailsResponse(string firstName, string lastName, string phone, TripStatus rideStatus, string pickupLocation, string dropLocation)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            RideStatus = rideStatus;
            PickupLocation = pickupLocation;
            DropLocation = dropLocation;
        }
    }
}
