using Assignment_2.Entity;

namespace Assignment_2.Repository.IRepository
{
    public interface IAuthRepository
    {
        void AddUser(UserAuth userAuth);
        List<UserAuth> GetUsersAuthList();
    }
}
