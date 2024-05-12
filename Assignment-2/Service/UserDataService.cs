using Assignment_2.CustomException;
using Assignment_2.Entity;
using Assignment_2.Repository;

namespace Assignment_2.Service
{
    public class UserDataService
    {
        private UserDataRepository _userDataRepository;

        public UserDataService(UserDataRepository userDataRepository)
        {
            _userDataRepository = userDataRepository;
        }

        public void AddUserData(UserData userData)
        {
            _userDataRepository.AddUser(userData);
        }

        public bool IsEmailUnique(string email)
        {
            var userDataList = _userDataRepository.GetUsersDataList();

            if (userDataList.Any(user => user.Email == email))
            {
                throw new EmailAlreadyExistsException("User with given email address already exists.");
            }

            return true;
        }

        public bool IsUsernameUnique(string username)
        {
            var userDataList = _userDataRepository.GetUsersDataList();

            if (userDataList.Any(user => user.Username == username))
            {
                throw new UsernameAlreadyExistsException("Username already exists.");
            }

            return true;
        }

        public UserData GetUserData(string username)
        {
            var userDataList = _userDataRepository.GetUsersDataList();

            var userData = userDataList.FirstOrDefault(u => u.Username == username);

            if (userData == null)
            {
                throw new UserNotFoundException($"User data not found for username: {username}");
            }

            return userData;
        }
    }
}
