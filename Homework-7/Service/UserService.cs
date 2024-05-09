using Homework_7.Entity;
using Homework_7.Repository;
using Newtonsoft.Json;

namespace Homework_7.Service
{
    public class UserService
    {

        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void RegisterUser(User user)
        {
            Console.WriteLine("user service: ", user);
            Console.WriteLine(JsonConvert.SerializeObject(user));
            _userRepository.SaveUser(user);
        }

        public List<User> GetAllUsers() {
            Console.WriteLine("service start 1");
            var random = _userRepository.GetAllUsers();
            Console.WriteLine(JsonConvert.SerializeObject(random));
            return random;
        }
    }
}
