using Assessment_1.Entities;
using Assessment_1.Enums;
using Assessment_1.Models;

namespace Assessment_1.Mappers
{
    public static class DriverMapper
    {
        public static Driver ConvertDriverRegisterRequestToDriver(Guid userId, DriverRegisterRequest driverRegisterRequest)
        {
            return new Driver(userId, driverRegisterRequest.PlateNumber, driverRegisterRequest.VehicleType, driverRegisterRequest.LicenseNumber, DriverAvailability.Unavailable);
        }
    }
}
