using Assessment_1.Entities;
using Assessment_1.Enums;
using Assessment_1.Exceptions;
using Assessment_1.Mappers;
using Assessment_1.Models;
using Assessment_1.Repositories.IRepositories;
using Assessment_1.Services.IServices;

namespace Assessment_1.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IUserService _userService;
        private readonly IBookingService _bookingService;
        private readonly IDriverService _driverService;
        private readonly IRatingService _ratingService;

        public TripService(ITripRepository tripRepository, IUserService userService, IBookingService bookingService, IDriverService driverService, IRatingService ratingService)
        {
            _tripRepository = tripRepository;
            _userService = userService;
            _bookingService = bookingService;
            _driverService = driverService;
            _ratingService = ratingService;
        }

        public Guid AddTrip(Trip trip)
        {
            try
            {
                return _tripRepository.AddTrip(trip);
            }
            catch (Exception ex)
            {
                throw new TripOperationException("Failed to add Trip", ex);
            }
        }

        public Trip GetCurrentRiderTrip(Guid riderId)
        {
            return GetOngoingTrip(_tripRepository.GetCurrentRiderTrip(riderId));
        }

        public Trip GetCurrentDriverTrip(Guid driverId)
        {
            return GetOngoingTrip(_tripRepository.GetCurrentDriverTrip(driverId));
        }

        private Trip GetOngoingTrip(Trip? trip)
        {
            if (trip == null)
                throw new TripNotFoundException("No ongoing ride available");

            return trip;
        }

        public void CancelTrip(CancelTripRequest cancelTripRequest, string email, UserRole userRole)
        {
            Trip trip = ValidateTripForAction(cancelTripRequest.TripId, email, userRole, TripStatus.Confirmed);

            SetDriverAvailabilityToAvailable(trip.DriverId);
            UpdateTripStatus(trip, TripStatus.Canceled);
            CreateAndAddBooking(trip, TripStatus.Canceled);
            TryDeleteTrip(trip);
        }

        public void EndTrip(EndTripRequest endTripRequest, string email, UserRole userRole)
        {
            Trip trip = ValidateTripForAction(endTripRequest.TripId, email, userRole, TripStatus.Ongoing);

            SetDriverAvailabilityToAvailable(trip.DriverId);
            UpdateTripStatus(trip, TripStatus.Completed);
            UpdateEndDT(trip);
            Guid bookingId = CreateAndAddBooking(trip, TripStatus.Completed);
            _ratingService.AddRating(bookingId);
            TryDeleteTrip(trip);
        }

        public void StartRide(StartTripRequest startTripRequest, string email, UserRole userRole)
        {
            Trip trip = ValidateTripForAction(startTripRequest.TripId, email, userRole, TripStatus.Confirmed);

            ValidateOtp(startTripRequest.OTP, trip.OTP);
            UpdateStartDT(trip);
            UpdateTripStatus(trip, TripStatus.Ongoing);
        }

        public Trip GetTrip(Guid tripId)
        {
            Trip? trip = _tripRepository.GetTripByTripId(tripId);

            if (trip == null)
                throw new TripNotFoundException("Trip not found");

            return trip;
        }

        public DriverTripDetailsResponse GetCurrentTripDetails(string userEmail)
        {
            User driver = _userService.GetUserFromEmailAndRole(userEmail, UserRole.Driver);
            Trip trip = GetCurrentDriverTrip(driver.UserId);
            Guid riderId = trip.RiderId;
            User rider = _userService.GetUserById(riderId);

            return TripMapper.CreateDriverTripDetailsResponseFromTripAndRider(trip, rider);
        }

        private Trip ValidateTripForAction(Guid tripId, string email, UserRole userRole, TripStatus expectedStatus)
        {
            Trip trip = GetTrip(tripId);

            if (trip.RideStatus != expectedStatus)
                throw new TripOperationException($"Cannot perform action on a trip that is not {expectedStatus}");

            User user = _userService.GetUserFromEmailAndRole(email, userRole);

            if (!IsUserAssociatedWithTrip(user, trip))
                throw new TripNotFoundException("Trip not found.");

            return trip;
        }

        private void SetDriverAvailabilityToAvailable(Guid driverUserId)
        {
            Driver driver = _driverService.GetDriverById(driverUserId);
            _driverService.SetDriverAvailability(driver.DriverId, DriverAvailability.Available);
        }

        private Guid CreateAndAddBooking(Trip trip, TripStatus tripStatus)
        {
            Booking booking = BookingMapper.CreateBookingFromTrip(trip, tripStatus);

            try
            {
                Guid bookingId = _bookingService.AddBooking(booking);
                return bookingId;
            }
            catch (FailedToAddBookingException ex)
            {
                throw new TripOperationException("Failed to cancel trip", ex);
            }
        }

        private void TryDeleteTrip(Trip trip)
        {
            try
            {
                DeleteTrip(trip);
            }
            catch (Exception ex)
            {
                throw new TripOperationException("Failed to cancel trip", ex);
            }
        }

        private bool IsUserAssociatedWithTrip(User user, Trip trip)
        {
            return trip.RiderId == user.UserId || trip.DriverId == user.UserId;
        }

        private void DeleteTrip(Trip trip)
        {
            try
            {
                _tripRepository.DeleteTrip(trip);
            }
            catch (Exception ex)
            {
                throw new TripOperationException("Failed to delete trip", ex);
            }
        }

        private void UpdateStartDT(Trip trip)
        {
            try
            {
                _tripRepository.UpdateStartDT(trip);
            }
            catch (Exception ex)
            {
                throw new TripOperationException("Failed to update trip start time", ex);
            }
        }

        private void UpdateEndDT(Trip trip)
        {
            try
            {
                _tripRepository.UpdateEndDT(trip);
            }
            catch (Exception ex)
            {
                throw new TripOperationException("Failed to update trip end time", ex);
            }
        }

        private void UpdateTripStatus(Trip trip, TripStatus tripStatus)
        {
            try
            {
                _tripRepository.UpdateTripStatus(trip, tripStatus);
            }
            catch (Exception ex)
            {
                throw new TripOperationException("Failed to update trip status", ex);
            }
        }

        private void ValidateOtp(string inputOtp, string actualOtp)
        {
            if (inputOtp != actualOtp)
                throw new InvalidOtpException("Otp is incorrect.");
        }
    }
}
