using Assessment_1.Entities;
using Assessment_1.Enums;
using Assessment_1.Exceptions;
using Assessment_1.Mappers;
using Assessment_1.Models;
using Assessment_1.Repositories.IRepositories;
using Assessment_1.Services.IServices;
using Assessment_1.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace Assessment_1.Services
{
    public class DriverService : IDriverService
    {
        
        private IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public Driver FindAvailableDriver(VehicleType vehicleType)
        {
            Driver? availableDriver = _driverRepository.FindAvailableDriver(vehicleType);

            if(availableDriver == null )
            {
                throw new DriverNotFoundException("No available driver found.");
            }
            
            return availableDriver;
        }

        public void SetDriverAvailability(Guid driverId, DriverAvailability driverAvailability)
        {
            bool IsAvailabilitySet = _driverRepository.SetDriverAvailability(driverId, driverAvailability);

            if (!IsAvailabilitySet)
            {
                throw new DriveNotFoundException("Driver not found");
            }
        }

        public DriverAvailability ToggleAvailability(string email)
        {
            Driver? driver = _driverRepository.ToggleAvailability(email);

            if (driver == null)
            {
                throw new DriveNotFoundException("Driver not found");
            }
            
            if(driver.Availability == DriverAvailability.InARide)
            {
                throw new FailedToUpdateDriverAvailabilityException("Driver is in a ride. Can't toggel availability.");
            }

            return driver.Availability;
        }

        public Driver GetDriverById(Guid driverId)
        {
            Driver? driver = _driverRepository.GetDriver(driverId);

            if (driver == null)
            {
                throw new DriverNotFoundException("Driver not found");
            }

            return driver;
        }
    }
}
