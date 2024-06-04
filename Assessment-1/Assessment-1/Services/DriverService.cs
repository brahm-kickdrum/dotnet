using Assessment_1.Entities;
using Assessment_1.Exceptions;
using Assessment_1.Repositories.IRepositories;
using Assessment_1.Services.IServices;
using Assessment_1.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assessment_1.Services
{
    public class DriverService : IDriverService
    {
        private IConfiguration _config;
        private IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository, IConfiguration config)
        {
            _driverRepository = driverRepository;
            _config = config;
        }
        public Guid AddDriver(Driver driver)
        {
            return _driverRepository.AddDriver(driver);
        }

        public string LoginDriver(string email, string password)
        {
            Driver driver = _driverRepository.GetDriverByEmail(email);
            bool authenticatedDriver = AuthenticateDriver(driver, password);
            string token = "";
            if (authenticatedDriver)
            {
                token = GenerateToken(email, Constants.DRIVER);
            }
            else
            {
                throw new InvalidPasswordException("Invalid driver password");
            }
            return token;

        }

        public bool AuthenticateDriver(Driver driver, string password)
        {
            if (driver.Password != driver.Password)
            {
                return false;
            }

            return true;
        }

        public string GenerateToken(string email, string role)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(ClaimTypes.Role, role)
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
    }
}
