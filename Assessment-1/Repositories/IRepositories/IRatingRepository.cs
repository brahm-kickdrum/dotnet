using Assessment_1.Entities;

namespace Assessment_1.Repositories.IRepositories
{
    public interface IRatingRepository
    {
        Guid AddRating(Rating rating);

        Rating? GetRatingByTripId(Guid tripId);

        void AddRiderRating(Rating rating, int riderRating);

        void AddDriverRating(Rating rating, int driverRating);
    }
}
