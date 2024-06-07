using Assessment_1.Entities;
using Assessment_1.Enums;
using Assessment_1.Models;

namespace Assessment_1.Services.IServices
{
    public interface IDriverService
    {
        Driver FindAvailableDriver(VehicleType vehicleType);

        void SetDriverAvailability(Guid driverId, DriverAvailability driverAvailability);

        DriverAvailability ToggleAvailability(string email);

        Driver GetDriverById(Guid driverId);
    }
}
