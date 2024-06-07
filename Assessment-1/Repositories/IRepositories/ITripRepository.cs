using Assessment_1.Entities;
using Assessment_1.Enums;

namespace Assessment_1.Repositories.IRepositories
{
    public interface ITripRepository
    {
        Guid AddTrip(Trip trip);

        Trip? GetCurrentRiderTrip(Guid riderId);

        Trip? GetTripByTripId(Guid tripId);

        Trip? GetCurrentDriverTrip(Guid driverId);

        void DeleteTrip(Trip trip);

        void UpdateStartDT(Trip trip);

        void UpdateEndDT(Trip trip);

        void UpdateTripStatus(Trip trip, TripStatus tripStatus);
    }
}
