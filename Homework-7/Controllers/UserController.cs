using Homework_7.Dto;
using Homework_7.Entity;
using Homework_7.Mappers;
using Homework_7.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Homework_7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRequestDto userDto)
        {
            Console.WriteLine($"userDto : {userDto}");
            Console.WriteLine(JsonConvert.SerializeObject(userDto));
            User user = UserMapper.MapUserRequestDtoToUser(userDto);
            Console.WriteLine($"user controller : {user}");
            Console.WriteLine(JsonConvert.SerializeObject(user));
            _userService.RegisterUser(user);
            return Ok();
        }


        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var userList = _userService.GetAllUsers();
            Console.WriteLine("controller start");
            Console.WriteLine(JsonConvert.SerializeObject(userList));
            var userDtoList = UserMapper.MapUserListToUserResponseDtoList(userList);
            Console.WriteLine(JsonConvert.SerializeObject(userDtoList));
            return Ok(userDtoList);
        }
    }
}
