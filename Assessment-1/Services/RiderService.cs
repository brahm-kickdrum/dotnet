using Assessment_1.Entities;
using Assessment_1.Enums;
using Assessment_1.Exceptions;
using Assessment_1.Mappers;
using Assessment_1.Models;
using Assessment_1.Services.IServices;
using Assessment_1.Utils;

namespace Assessment_1.Services
{
    public class RiderService : IRiderService
    {
        private IUserService _userService;
        private IDriverService _driverService;
        private ITripService _tripService;

        public RiderService(IUserService userService, IDriverService driverService, ITripService tripService)
        {
            _userService = userService;
            _driverService = driverService;
            _tripService = tripService;
        }

        public TripRequestResponse RequestTrip(TripRequest tripRequest, string userEmail, UserRole userRole)
        {
            User rider = _userService.GetUserFromEmailAndRole(userEmail, userRole);
            Guid riderId = rider.UserId;

            try
            {
                Trip ongoingTrip = _tripService.GetCurrentRiderTrip(riderId);
                throw new TripOperationException("The rider is already in an ongoing trip.");
            }
            catch (TripNotFoundException)
            {
                Driver availableDriver = _driverService.FindAvailableDriver(tripRequest.RideType);
                Guid driverId = availableDriver.UserId;

                _driverService.SetDriverAvailability(availableDriver.DriverId, DriverAvailability.InARide);

                string otp = OtpGenerator.GenerateOtp();

                Trip trip = TripMapper.CreateTripFromTripRequest(tripRequest, riderId, driverId, otp);

                _tripService.AddTrip(trip);

                return TripMapper.CreateTripRequestResponseFromTripAndDriver(trip, availableDriver, otp);
            }
        }

        public RiderTripDetailsResponse GetCurrentTripDetails (string userEmail) 
        {
            User rider = _userService.GetUserFromEmailAndRole(userEmail, UserRole.Rider);
            Trip trip = _tripService.GetCurrentRiderTrip(rider.UserId);
            Driver driver = _driverService.GetDriverById(trip.DriverId);
            string? otp = null;

            if(trip.RideStatus == TripStatus.Confirmed)
            {
                otp = trip.OTP;
            }

            return TripMapper.CreateTripDetailsResponseFromTripAndDriver(trip, driver, otp);
        }
    }
}
