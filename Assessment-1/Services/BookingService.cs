using Assessment_1.Entities;
using Assessment_1.Exceptions;
using Assessment_1.Repositories;
using Assessment_1.Repositories.IRepositories;
using Assessment_1.Services.IServices;

namespace Assessment_1.Services
{
    public class BookingService : IBookingService
    {
        private IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public Guid AddBooking(Booking booking)
        {
            try
            {
                Guid bookingId = _bookingRepository.AddBooking(booking);
                return bookingId;
            }
            catch (Exception ex)
            {
                throw new FailedToAddBookingException("Failed to add Booking", ex);
            }
        }

        public Booking GetBookingByTripId(Guid tripId)
        {
            Booking? booking = _bookingRepository.GetBookingByTripId(tripId);

            if (booking == null)
            {
                throw new BookingNotFoundException("Booking not found");
            }

            return booking;
        }
    }
}
