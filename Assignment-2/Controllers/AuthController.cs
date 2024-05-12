using Assignment_2.CustomException;
using Assignment_2.Dto;
using Assignment_2.Entity;
using Assignment_2.Mapper;
using Assignment_2.Service;
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
        private AuthService _authService;
        private UserDataService _userDataService;
        public AuthController(AuthService authService, UserDataService userDataService)
        {
            _authService = authService;
            _userDataService = userDataService;
        }

        [HttpPost("register")]
        public ActionResult<string> Register(UserRegisterRequestDto userRegisterRequest)
        {
            try
            {
                if (_userDataService.IsUsernameUnique(userRegisterRequest.Username) && _userDataService.IsEmailUnique(userRegisterRequest.Email))
                {
                    UserAuth userAuth = UserAuthMapper.MapToUserAuth(userRegisterRequest);
                    _authService.AddUserAuth(userAuth);
                    UserData userData = UserDataMapper.MapToUserData(userRegisterRequest);
                    _userDataService.AddUserData(userData);
                }
            }
            catch (UsernameAlreadyExistsException ex)
            {
                return StatusCode(409, ex.Message);
            }
            catch (EmailAlreadyExistsException ex)
            {
                return StatusCode(409, ex.Message);
            }

            return Ok("User registered successfully");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> Login(UserAuth userRequestDto)
        {
            try
            {
                UserAuth user = userRequestDto;
                var username = _authService.AutheticateUser(user);
                if (username != "")
                {
                    var token = _authService.GenerateToken(username);
                    return Ok(token);
                }
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidPasswordException ex)
            {
                return Unauthorized(ex.Message);
            }

            return BadRequest("Invalid username or password");
        }

    }
}