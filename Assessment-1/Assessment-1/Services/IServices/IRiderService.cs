using Assessment_1.Entities;

namespace Assessment_1.Services.IServices
{
    public interface IRiderService
    {
        Guid AddRider(Rider rider);
        string LoginRider(string email, string password);
        bool AuthenticateRider(Rider rider, string password);
        string GenerateToken(string email, string role);
    }
}
