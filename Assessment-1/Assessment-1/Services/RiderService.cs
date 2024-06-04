using Assessment_1.Entities;
using Assessment_1.Exceptions;
using Assessment_1.Repositories.IRepositories;
using Assessment_1.Services.IServices;
using Assessment_1.Utils;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assessment_1.Services
{
    public class RiderService : IRiderService
    {
        private IConfiguration _config;
        private IRiderRepository _riderRepository;
        public RiderService(IRiderRepository riderRepository , IConfiguration config)
        {
            _riderRepository = riderRepository;
            _config = config;
        }
        public Guid AddRider(Rider rider)
        {
            return _riderRepository.AddRider(rider);
        }

        public string LoginRider(string email, string password)
        {
            Rider rider = _riderRepository.GetRiderByEmail(email);
            bool authenticatedRider = AuthenticateRider(rider, password);
            string token = "";
            if(authenticatedRider)
            {
               token = GenerateToken(email, Constants.RIDER);
            }
            else
            {
                throw new InvalidPasswordException("Invalid rider password");
            }
            return token;

        }

        public bool AuthenticateRider(Rider rider, string password)
        {
            if (rider.Password != rider.Password)
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
