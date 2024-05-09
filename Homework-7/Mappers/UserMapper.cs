using Homework_7.Dto;
using Homework_7.Entity;

namespace Homework_7.Mappers
{
    public class UserMapper
    {
        public static User MapUserRequestDtoToUser(UserRequestDto userRequestDto)
        {
            if (userRequestDto == null)
                return null;

            return new User
            {
                Username = userRequestDto.Username,
                Email = userRequestDto.Email,
                Password = userRequestDto.Password,
                ConfirmPassword = userRequestDto.ConfirmPassword,
                Age = userRequestDto.Age,
                PhoneNumber = userRequestDto.PhoneNumber,
                Country = userRequestDto.Country,
                Gender = userRequestDto.Gender,
                CreditCardNumber = userRequestDto.CreditCardNumber,
                ExpirationDate = userRequestDto.ExpirationDate,
                CVV = userRequestDto.CVV ?? 0
            };
        }

        public static List<UserResponseDto> MapUserListToUserResponseDtoList(List<User> users)
        {
            List<UserResponseDto> userResponseDtos = new List<UserResponseDto>();

            foreach (var user in users)
            {
                var userResponseDto = new UserResponseDto
                {
                    Username = user.Username,
                    Email = user.Email,
                    Age = user.Age,
                    PhoneNumber = user.PhoneNumber
                };

                userResponseDtos.Add(userResponseDto);
            }

            return userResponseDtos;
        }
    }
}
