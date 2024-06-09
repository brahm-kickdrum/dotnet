using Assignment_2.CustomException;
using Assignment_2.Dto;
using Assignment_2.Entity;
using Assignment_2.Mapper;
using Assignment_2.Service;
using Assignment_2.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment_2.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IUserDataService _userDataService;
        public AuthController(IAuthService authService, IUserDataService userDataService)
        {
            _authService = authService;
            _userDataService = userDataService;
        }

        [HttpPost("register")]
        public ActionResult<string> Register(UserRegisterRequestViewModel userRegisterRequest)
        {
            if (_userDataService.IsUsernameUnique(userRegisterRequest.Username) && _userDataService.IsEmailUnique(userRegisterRequest.Email))
            {
                UserAuth userAuth = UserAuthMapper.MapToUserAuth(userRegisterRequest);
                _authService.AddUserAuth(userAuth);
                UserData userData = UserDataMapper.MapToUserData(userRegisterRequest);
                _userDataService.AddUserData(userData);
            }

            return Ok("User registered successfully");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> Login(UserLoginRequestViewModel userRequestViewModel)
        {
            UserAuth user = UserAuthMapper.MapUserLoginRequestViewModelToUserAuth(userRequestViewModel);
            string username = _authService.AutheticateUser(user);
            if (username != "")
            {
                string token = _authService.GenerateToken(username);
                return Ok(token);
            }

            return BadRequest("Invalid username or password");
        }
    }
}