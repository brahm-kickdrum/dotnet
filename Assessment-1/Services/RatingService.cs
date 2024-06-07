using Assessment_1.Entities;
using Assessment_1.Enums;
using Assessment_1.Exceptions;
using Assessment_1.Mappers;
using Assessment_1.Models;
using Assessment_1.Repositories;
using Assessment_1.Repositories.IRepositories;
using Assessment_1.Services.IServices;

namespace Assessment_1.Services
{
    public class RatingService : IRatingService
    {
        private IRatingRepository _ratingRepository;
        private IBookingService _bookingService;
        private IUserService _userService;

        public RatingService(IRatingRepository ratingRepository, IBookingService bookingService, IUserService userService)
        {
            _ratingRepository = ratingRepository;
            _bookingService = bookingService;
            _userService = userService;
        }

        public Guid AddRating(Guid bookingId)
        {
            Rating rating = new Rating(bookingId);

            if (rating == null)
            {
                throw new FailedToAddRatingsException("Failed to create rating.");
            }

            try
            {
                Guid ratingId = _ratingRepository.AddRating(rating);
                return ratingId;
            }
            catch (Exception ex)
            {
                throw new FailedToAddRatingsException("Failed to add ratings.", ex);
            }
        }

        public void UpdateRiderRating(RatingRequest ratingRequest, string email, UserRole userRole)
        {
            Rating rating = ValidateAndRetrieveRating(ratingRequest.TripId, email, userRole);
            _ratingRepository.AddRiderRating(rating, ratingRequest.Rating);
        }

        public void UpdateDriverRating(RatingRequest ratingRequest, string email, UserRole userRole)
        {
            Rating rating = ValidateAndRetrieveRating(ratingRequest.TripId, email, userRole);
            _ratingRepository.AddDriverRating(rating, ratingRequest.Rating);
        }

        private Rating ValidateAndRetrieveRating(Guid tripId, string email, UserRole userRole)
        {
            User user = _userService.GetUserFromEmailAndRole(email, userRole);
            Booking booking = _bookingService.GetBookingByTripId(tripId);

            if (userRole == UserRole.Driver && booking.DriverId != user.UserId)
                throw new TripNotFoundException("Trip not found.");

            if (userRole == UserRole.Rider && booking.RiderId != user.UserId)
                throw new TripNotFoundException("Trip not found.");

            Rating? rating = _ratingRepository.GetRatingByTripId(booking.BookingId);
            if (rating == null)
                throw new RatingNotFoundException("Cannot rate the ride.");

            return rating;
        }
    }
}
