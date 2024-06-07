using Assessment_1.DataAccess;
using Assessment_1.Entities;
using Assessment_1.Repositories.IRepositories;

namespace Assessment_1.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly RideDbContext _dbContext;

        public BookingRepository(RideDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid AddBooking(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            _dbContext.SaveChanges();

            return booking.BookingId;
        }

        public Booking? GetBookingByTripId(Guid tripId)
        {
            Booking? booking = _dbContext.Bookings.FirstOrDefault(b => b.TripId == tripId);
            return booking;
        }
    }
}
