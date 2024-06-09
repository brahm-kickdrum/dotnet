using Assessment_1.Entities;
using Assessment_1.Enums;
using Assessment_1.Models;

namespace Assessment_1.Services.IServices
{
    public interface ITripService
    {
        Guid AddTrip(Trip trip);

        Trip GetCurrentRiderTrip(Guid riderId);

        void CancelTrip(CancelTripRequest cancelTripRequest, string email, UserRole userRole);

        void EndTrip(EndTripRequest endTripRequest, string email, UserRole userRole);

        void StartRide(StartTripRequest startTripRequest, string email, UserRole userRole);

        DriverTripDetailsResponse GetCurrentTripDetails(string userEmail);
    }
}
