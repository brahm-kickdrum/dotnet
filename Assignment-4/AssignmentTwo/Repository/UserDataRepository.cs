using Assignment_2.Entity;
using Assignment_2.Repository.IRepository;

namespace Assignment_2.Repository
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly List<UserData> _users;

        public UserDataRepository()
        {
            _users = new List<UserData>();
        }

        public void AddUser(UserData user)
        {
            _users.Add(user);
            Console.WriteLine("User data added successfully.");
        }

        public List<UserData> GetUsersDataList()
        {
            Console.WriteLine("List of user data returned successfully.");
            return _users;
        }
    }
}
