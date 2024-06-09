using Assessment_1.Entities;
using Assessment_1.Enums;
using Assessment_1.Exceptions;
using Assessment_1.Mappers;
using Assessment_1.Models;
using Assessment_1.Repositories.IRepositories;
using Assessment_1.Services.IServices;
using Assessment_1.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assessment_1.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepository;
        private IDriverRepository _driverRepository;
        private ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, IDriverRepository driverRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _driverRepository = driverRepository;
            _tokenService = tokenService;
        }

        public Guid AddRider(UserRegisterRequest userRegisterRequest)
        {
            User user = UserMapper.ConvertRiderRegisterRequestToUser(userRegisterRequest);
            return AddUser(user, UserRole.Rider);
        }

        public Guid AddUserAndDriver(DriverRegisterRequest driverRegisterRequest)
        {
            User user = UserMapper.ConvertDriverRegisterRequestToUser(driverRegisterRequest);
            Guid userId = AddUser(user, UserRole.Driver);
            AddDriver(userId, driverRegisterRequest);
            return userId;
        }

        public Guid AddUser(User user, UserRole role)
        {
            bool userExists = _userRepository.CheckIfUserExists(user, role);

            if (userExists)
            {
                throw new UserAlreadyExistsException("User already exists.");
            }

            try
            {
                return _userRepository.AddUser(user);
            }
            catch (Exception ex)
            {
                throw new FailedToAddUserException("Failed to add the user.", ex);
            }
        }

        public Guid AddDriver(Guid userId, DriverRegisterRequest driverRegisterRequest)
        {
            Driver driver = DriverMapper.ConvertDriverRegisterRequestToDriver(userId, driverRegisterRequest);

            try
            {
                return _driverRepository.AddDriver(driver);
            }
            catch (Exception ex)
            {
                throw new FailedToAddDriverException("Failed to add the driver.", ex);
            }
        }

        public string LoginUser(UserLoginRequest userLoginRequest)
        {
            string email = userLoginRequest.Email;
            string password = userLoginRequest.Password;
            UserRole role = userLoginRequest.Role;

            User user = FetchAndValidateUser(email, role);

            CheckIfUserIsAvailableAsDriver(email, role, user);

            bool isUserAuthenticated = AuthenticateUser(user, password);

            if(!isUserAuthenticated)
            {
                throw new InvalidPasswordException("Invalid user password");
            }

            return _tokenService.GenerateToken(email, role);
        }

        public bool AuthenticateUser(User user, string password)
        {
            if (user.Password != user.Password)
            {
                return false;
            }

            return true;
        }

        private User FetchAndValidateUser(string email, UserRole role)
        {
            User? user = _userRepository.GetUserByEmailAndRole(email, role);

            if (user == null)
            {
                throw new UserNotFoundException("Invalid user email");
            }

            return user;
        }

        private void CheckIfUserIsAvailableAsDriver(string email, UserRole role, User user)
        {
            if (role == UserRole.Rider)
            {
                User? userAsDriver = _userRepository.GetUserByEmailAndRole(email, UserRole.Driver);
                Driver? driver = _driverRepository.GetDriver(user.UserId);

                if (driver != null && driver.Availability == DriverAvailability.Available)
                {
                    throw new UserUnavailableForRoleException("User is currently available as a driver and cannot log in as a rider.");
                }
            }
        }
    }
}
