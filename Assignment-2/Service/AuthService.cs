using Assignment_2.CustomException;
using Assignment_2.Entity;
using Assignment_2.Repository;
using Assignment_2.Repository.IRepository;
using Assignment_2.Service.IService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment_2.Service
{
    public class AuthService : IAuthService
    {
        private IAuthRepository _authRepository;
        private IConfiguration _config;

        public AuthService(IAuthRepository authRepository, IConfiguration config)
        {
            _authRepository = authRepository;
            _config = config;
        }

        public string AutheticateUser(UserAuth user)
        {
            List<UserAuth> userAuthList = _authRepository.GetUsersAuthList();

            UserAuth authenticatedUser = userAuthList.FirstOrDefault(u => u.Username == user.Username);

            if (authenticatedUser == null)
            {
                throw new UserNotFoundException("User not found");
            }
            else if(authenticatedUser.Password != user.Password)
            {
                throw new InvalidPasswordException("Invalid password");
            }

            return authenticatedUser.Username;
        }

        public string GenerateToken(string username)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username)
            };

            JwtSecurityToken Sectoken = new JwtSecurityToken(
                  _config["Jwt:Issuer"],
                  _config["Jwt:Audience"],
                  claims: claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

            string token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return token;
        }

        public void AddUserAuth(UserAuth userAuth)
        {
            _authRepository.AddUser(userAuth);
        }

    }
}
