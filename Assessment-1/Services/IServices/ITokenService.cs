using Assessment_1.Enums;

namespace Assessment_1.Services.IServices
{
    public interface ITokenService
    {
        string GenerateToken(string email, UserRole role);
    }
}
