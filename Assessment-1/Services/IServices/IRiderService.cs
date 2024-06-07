using Assessment_1.Enums;
using Assessment_1.Models;

namespace Assessment_1.Services.IServices
{
    public interface IRiderService
    {
        TripRequestResponse RequestTrip(TripRequest tripRequest, string userEmail, UserRole userRole);

        RiderTripDetailsResponse GetCurrentTripDetails(string userEmail);
    }
}
