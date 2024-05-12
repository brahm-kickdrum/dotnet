using Assignment_2.Dto;
using Assignment_2.Entity;

namespace Assignment_2.Mapper
{
    public class UserDataMapper
    {
        public static UserData MapToUserData(UserRegisterRequestDto userRegisterRequestDto)
        {
            return new UserData
            {
                Username = userRegisterRequestDto.Username,
                Name = userRegisterRequestDto.Name,
                Email = userRegisterRequestDto.Email,
                Address = userRegisterRequestDto.Address,
                PhoneNumber = userRegisterRequestDto.PhoneNumber
            };
        }
    }
}
