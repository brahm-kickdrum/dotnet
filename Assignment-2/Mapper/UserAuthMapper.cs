using Assignment_2.Dto;
using Assignment_2.Entity;

namespace Assignment_2.Mapper
{
    public class UserAuthMapper
    {
        public static UserAuth MapToUserAuth(UserRegisterRequestDto userRegisterRequestDto)
        {
            return new UserAuth
            {
                Username = userRegisterRequestDto.Username,
                Password = userRegisterRequestDto.Password
            };
        }
    }
}
