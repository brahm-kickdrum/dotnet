using Assessment_1.Entities;
using Assessment_1.Enums;
using Assessment_1.Models;

namespace Assessment_1.Mappers
{
    public static class TripMapper
    {
        public static Trip CreateTripFromTripRequest(TripRequest tripRequest, Guid riderId, Guid driverId, string otp)
        {
            return new Trip(riderId, driverId, tripRequest.PickupLocation, tripRequest.DropLocation, otp, TripStatus.Confirmed);
        }

        public static TripRequestResponse CreateTripRequestResponseFromTripAndDriver(Trip trip, Driver driver, string otp)
        {
            return new TripRequestResponse(trip.TripId, driver.PlateNumber, driver.VehicleType, driver.LicenseNumber, otp, TripStatus.Confirmed);
        }

        public static RiderTripDetailsResponse CreateTripDetailsResponseFromTripAndDriver(Trip trip, Driver driver, string? otp)
        {
            return new RiderTripDetailsResponse(driver.User.FirstName, driver.User.LastName, driver.User.Phone, driver.PlateNumber, driver.VehicleType, driver.LicenseNumber, trip.RideStatus, otp);
        }

        public static DriverTripDetailsResponse CreateDriverTripDetailsResponseFromTripAndRider(Trip trip, User user)
        {
            return new DriverTripDetailsResponse(user.FirstName, user.LastName, user.Phone, trip.RideStatus, trip.PickupLocation, trip.DropLocation);
        }
    }
}
