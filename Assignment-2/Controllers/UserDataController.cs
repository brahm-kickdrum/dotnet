using Assignment_2.CustomException;
using Assignment_2.Entity;
using Assignment_2.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Assignment_2.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private UserDataService _userDataService;

        public UserDataController(UserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        [Authorize]
        [HttpGet("get-user")]
        public ActionResult<UserData> getUser()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userData = new UserData();

            if (username == null)
            {
                return NotFound("User not found");
            }
            try
            {
                userData = _userDataService.GetUserData(username);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex);
            }

            return Ok(userData);
        }
    }
}
