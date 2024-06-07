using Assessment_1.Entities;
using Assessment_1.Enums;
using Assessment_1.Models;

namespace Assessment_1.Mappers
{
    public static class UserMapper
    {
        public static User ConvertRiderRegisterRequestToUser(UserRegisterRequest riderRegisterRequest)
        {
            return new User(riderRegisterRequest.FirstName, riderRegisterRequest.LastName, riderRegisterRequest.Email, riderRegisterRequest.Phone, riderRegisterRequest.Password, UserRole.Rider);
        }

        public static User ConvertDriverRegisterRequestToUser(DriverRegisterRequest driverRegisterRequest) 
        {
            return new User(driverRegisterRequest.FirstName, driverRegisterRequest.LastName, driverRegisterRequest.Email, driverRegisterRequest.Phone, driverRegisterRequest.Password, UserRole.Driver);  
        }
    }
}
