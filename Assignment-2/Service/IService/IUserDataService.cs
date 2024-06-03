using Assignment_2.Entity;

namespace Assignment_2.Service.IService
{
    public interface IUserDataService
    {
        void AddUserData(UserData userData);
        bool IsEmailUnique(string email);
        bool IsUsernameUnique(string username);
        UserData GetUserData(string username);
    }
}
