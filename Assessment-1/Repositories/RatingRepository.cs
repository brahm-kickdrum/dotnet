using Assessment_1.DataAccess;
using Assessment_1.Entities;
using Assessment_1.Repositories.IRepositories;

namespace Assessment_1.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly RideDbContext _dbContext;

        public RatingRepository(RideDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid AddRating(Rating rating)
        {
            _dbContext.Ratings.Add(rating);
            _dbContext.SaveChanges();
            return rating.RatingId;
        }

        public Rating? GetRatingByTripId(Guid tripId) 
        {
            Rating? rating = _dbContext.Ratings.FirstOrDefault(r => r.TripId == tripId);
            return rating;
        }

        public void AddRiderRating(Rating rating, int riderRating)
        {
            rating.RiderRating = riderRating;
            _dbContext.SaveChanges();
        }

        public void AddDriverRating(Rating rating, int driverRating)
        {
            rating.DriverRating = driverRating;
            _dbContext.SaveChanges();
        }
    }
}
