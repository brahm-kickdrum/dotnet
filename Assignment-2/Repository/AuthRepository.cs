using Assignment_2.Entity;

namespace Assignment_2.Repository
{
    public class AuthRepository
    {
        private readonly List<UserAuth> _userAuthList;

        public AuthRepository()
        {
            _userAuthList = new List<UserAuth>();
        }

        public void AddUser(UserAuth userAuth)
        {
            _userAuthList.Add(userAuth);
            Console.WriteLine("List of users:");
            foreach (var user in _userAuthList)
            {
                Console.WriteLine($"Username: {user.Username}, Password: {user.Password}");
            }
        }

        public List<UserAuth> GetUsersAuthList() {
            return _userAuthList;
        }

    }
}
