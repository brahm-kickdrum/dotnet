using Assessment_1.Entities;

namespace Assessment_1.Services.IServices
{
    public interface IBookingService
    {
        Guid AddBooking(Booking booking);

        Booking GetBookingByTripId(Guid tripId);
    }
}
