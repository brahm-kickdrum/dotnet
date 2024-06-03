using Assignment_2.Dto;
using Assignment_2.Entity;

namespace Assignment_2.Mapper
{
    public class UserAuthMapper
    {
        public static UserAuth MapToUserAuth(UserRegisterRequestViewModel userRegisterRequestViewModel)
        {
            return new UserAuth
            {
                Username = userRegisterRequestViewModel.Username,
                Password = userRegisterRequestViewModel.Password
            };
        }

        public static UserAuth MapUserLoginRequestViewModelToUserAuth(UserLoginRequestViewModel userLoginRequestViewModel)
        {
            return new UserAuth
            {
                Username = userLoginRequestViewModel.Username,
                Password = userLoginRequestViewModel.Password
            };
        }
    }
}
