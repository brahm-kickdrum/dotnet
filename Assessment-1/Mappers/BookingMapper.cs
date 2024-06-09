using Assessment_1.Entities;
using Assessment_1.Enums;

namespace Assessment_1.Mappers
{
    public static class BookingMapper
    {
        public static Booking CreateBookingFromTrip(Trip trip, TripStatus tripStatus)
        {
            return new Booking(trip.TripId, trip.RiderId, trip.DriverId, trip.PickupLocation, trip.DropLocation, trip.RideRequestDateTime, trip.StartDateTime, trip.EndDateTime, trip.Fare, tripStatus);
        }
    }
}
