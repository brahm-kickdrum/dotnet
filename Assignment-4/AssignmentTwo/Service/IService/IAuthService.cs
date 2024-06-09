using Assignment_2.Entity;

namespace Assignment_2.Service.IService
{
    public interface IAuthService
    {
        string AutheticateUser(UserAuth user);
        string GenerateToken(string username);
        void AddUserAuth(UserAuth userAuth);
    }
}
