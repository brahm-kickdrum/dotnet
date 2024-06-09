using Assessment_1.Enums;
using Assessment_1.Models;

namespace Assessment_1.Services.IServices
{
    public interface IRatingService
    {
        Guid AddRating(Guid tripId);

        void UpdateRiderRating(RatingRequest ratingRequest, string email, UserRole userRole);

        void UpdateDriverRating(RatingRequest ratingRequest, string email, UserRole userRole);
    }
}
