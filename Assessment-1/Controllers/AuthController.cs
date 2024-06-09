using Assessment_1.Enums;
using Assessment_1.Exceptions;
using Assessment_1.Mappers;
using Assessment_1.Models;
using Assessment_1.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Assessment_1.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService riderService)
        {
            _authService = riderService;
        }

        [AllowAnonymous]
        [HttpPost("rider/register")]
        public ActionResult<Guid> RegisterRider(UserRegisterRequest riderRegisterRequest)
        {
            try 
            { 
                Guid guid = _authService.AddRider(riderRegisterRequest);
                return Ok(guid);
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (FailedToAddUserException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("driver/register")]
        public ActionResult<Guid> RegisterDriver(DriverRegisterRequest driverRegisterRequest)
        {
            try
            {
                Guid guid = _authService.AddUserAndDriver(driverRegisterRequest);
                return Ok(guid);
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (FailedToAddUserException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (FailedToAddDriverException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> LoginUser(UserLoginRequest userLoginRequest)
        {
            try
            {
                string token = _authService.LoginUser(userLoginRequest); 
                return Ok(token);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidPasswordException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserUnavailableForRoleException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
