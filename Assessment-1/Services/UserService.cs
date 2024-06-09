using Assessment_1.Entities;
using Assessment_1.Enums;
using Assessment_1.Exceptions;
using Assessment_1.Repositories.IRepositories;
using Assessment_1.Services.IServices;

namespace Assessment_1.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService (IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }   

        public User GetUserFromEmailAndRole(string email, UserRole userRole) 
        {
            User? user = _userRepository.GetUserByEmailAndRole(email, userRole);

            if(user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            return user;
        }

        public User GetUserById(Guid id) 
        {
            User? user = _userRepository.GetUserById(id);

            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            return user;
        }
    }
}
