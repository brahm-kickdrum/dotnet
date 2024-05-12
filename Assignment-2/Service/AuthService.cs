using Assignment_2.CustomException;
using Assignment_2.Entity;
using Assignment_2.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment_2.Service
{
    public class AuthService
    {
        private AuthRepository _authRepository;
        private IConfiguration _config;

        public AuthService(AuthRepository authRepository, IConfiguration config)
        {
            _authRepository = authRepository;
            _config = config;
        }

        public string AutheticateUser(UserAuth user)
        {
            var userAuthList = _authRepository.GetUsersAuthList();

            var authenticatedUser = userAuthList.FirstOrDefault(u => u.Username == user.Username);

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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username)
            };

            var Sectoken = new JwtSecurityToken(
                  _config["Jwt:Issuer"],
                  _config["Jwt:Audience"],
                  claims: claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return token;
        }

        public void AddUserAuth(UserAuth userAuth)
        {
            _authRepository.AddUser(userAuth);
        }

    }
}
