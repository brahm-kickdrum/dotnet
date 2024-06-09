using Assessment_1.Entities;
using Assessment_1.Enums;

namespace Assessment_1.Repositories.IRepositories
{
    public interface IDriverRepository
    {
        Guid AddDriver(Driver driver);

        Driver? FindAvailableDriver(VehicleType vehicleType);

        bool SetDriverAvailability(Guid driverId, DriverAvailability driverAvailability);

        Driver? ToggleAvailability(string email);

        Driver? GetDriver(Guid driverId);
    }
}
