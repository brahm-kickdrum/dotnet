using Assignment_2.Entity;

namespace Assignment_2.Repository.IRepository
{
    public interface IUserDataRepository
    {
        void AddUser(UserData user);
        List<UserData> GetUsersDataList();
    }
}
