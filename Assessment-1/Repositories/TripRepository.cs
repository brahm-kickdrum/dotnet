using Assessment_1.DataAccess;
using Assessment_1.Entities;
using Assessment_1.Enums;
using Assessment_1.Repositories.IRepositories;

namespace Assessment_1.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly RideDbContext _dbContext;

        public TripRepository(RideDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid AddTrip(Trip trip)
        {
            _dbContext.Add(trip);
            _dbContext.SaveChanges();

            return trip.TripId;
        }

        public Trip? GetCurrentRiderTrip(Guid riderId)
        {
            Trip? trip = _dbContext.Trips.FirstOrDefault(t => t.RiderId == riderId);
            return trip;
        }

        public Trip? GetCurrentDriverTrip(Guid driverId)
        {
            Trip? trip = _dbContext.Trips.FirstOrDefault(t => t.DriverId == driverId);
            return trip;
        }

        public Trip? GetTripByTripId(Guid tripId)
        {
            Trip? trip = _dbContext.Trips.Find(tripId);
            return trip;
        }

        public void DeleteTrip(Trip trip)
        {
            _dbContext.Trips.Remove(trip);
            _dbContext.SaveChanges();
        }

        public void UpdateStartDT(Trip trip)
        {
            trip.StartDateTime = DateTime.UtcNow;
            _dbContext.SaveChanges();
        }

        public void UpdateEndDT(Trip trip)
        {
            trip.EndDateTime = DateTime.UtcNow;
            _dbContext.SaveChanges();
        }

        public void UpdateTripStatus(Trip trip, TripStatus tripStatus)
        {
            trip.RideStatus = tripStatus;
            _dbContext.SaveChanges();
        }
    }
}
