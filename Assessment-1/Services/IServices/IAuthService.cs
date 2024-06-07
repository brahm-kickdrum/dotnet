using Assessment_1.Entities;
using Assessment_1.Models;

namespace Assessment_1.Services.IServices
{
    public interface IAuthService
    {
        Guid AddRider(UserRegisterRequest riderRegisterRequest);

        Guid AddUserAndDriver(DriverRegisterRequest driverRegisterRequest);

        string LoginUser(UserLoginRequest userLoginRequest);
    }
}
