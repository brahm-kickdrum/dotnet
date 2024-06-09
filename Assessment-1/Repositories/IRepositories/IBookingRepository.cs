using Assessment_1.Entities;

namespace Assessment_1.Repositories.IRepositories
{
    public interface IBookingRepository
    {
        Guid AddBooking(Booking booking);

        Booking? GetBookingByTripId(Guid tripId);
    }
}
