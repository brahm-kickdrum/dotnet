using Assignment_2.Dto;
using Assignment_2.Entity;

namespace Assignment_2.Mapper
{
    public class UserDataMapper
    {
        public static UserData MapToUserData(UserRegisterRequestViewModel userRegisterRequestViewModel)
        {
            return new UserData
            {
                Username = userRegisterRequestViewModel.Username,
                Name = userRegisterRequestViewModel.Name,
                Email = userRegisterRequestViewModel.Email,
                Address = userRegisterRequestViewModel.Address,
                PhoneNumber = userRegisterRequestViewModel.PhoneNumber
            };
        }
    }
}
