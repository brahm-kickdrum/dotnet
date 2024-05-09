using Homework_7.Entity;
using Newtonsoft.Json;

namespace Homework_7.Repository
{
    public class UserRepository
    {
        private readonly List<User> _userInfoList = new();

        //public UserRepository() {
        //    _userInfoList = new List<User>();
        //} 

        public void SaveUser(User user)
        {
            //Console.WriteLine("user repo: ",user);
            Console.WriteLine(JsonConvert.SerializeObject(user));
            _userInfoList.Add(user);

            Console.WriteLine("User successfully added to the list.");
            Console.WriteLine("User repository - List after adding user:");
            Console.WriteLine(JsonConvert.SerializeObject(_userInfoList));
        }

        public List<User> GetAllUsers()
        {
            Console.WriteLine("user repo 2: ");
            Console.WriteLine(_userInfoList.Count);
            Console.WriteLine(JsonConvert.SerializeObject(_userInfoList));
            return _userInfoList;
        }
    }
}
