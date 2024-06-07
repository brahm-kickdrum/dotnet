using Assessment_1.DataAccess;
using Assessment_1.Entities;
using Assessment_1.Enums;
using Assessment_1.Exceptions;
using Assessment_1.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Assessment_1.Repositories
{
    public class DriverRepository : IDriverRepository 
    {
        private readonly RideDbContext _dbContext;

        public DriverRepository(RideDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid AddDriver(Driver driver)
        {
            _dbContext.Drivers.Add(driver);
            _dbContext.SaveChanges();

            return driver.DriverId;
        }

        public Driver? FindAvailableDriver(VehicleType vehicleType)
        {
            Driver? availableDriver = _dbContext.Drivers
                .Include(d => d.User) 
                .FirstOrDefault(d => d.VehicleType == vehicleType && d.Availability == DriverAvailability.Available);

            return availableDriver;
        }

        public bool SetDriverAvailability(Guid driverId, DriverAvailability driverAvailability)
        {
            Driver? driver = _dbContext.Drivers.Find(driverId);

            if (driver == null)
            {
                return false;
            }

            driver.Availability = driverAvailability;
            _dbContext.SaveChanges();

            return true;
        }

        public Driver? ToggleAvailability(string email)
        {
            Driver? driver = GetDriverByEmail(email);

            if(driver == null)
            {
                return driver;
            }
            else if (driver.Availability == DriverAvailability.Available)
            {
                driver.Availability = DriverAvailability.Unavailable;
                _dbContext.SaveChanges();
            }
            else if (driver.Availability == DriverAvailability.Unavailable)
            {
                driver.Availability = DriverAvailability.Available;
                _dbContext.SaveChanges();
            }

            return driver;
        }

        public Driver? GetDriverByEmail(string email)
        {
            Driver? driver = _dbContext.Drivers.FirstOrDefault(d => d.User.Email == email);
            return driver;
        }

        public Driver? GetDriver(Guid userId)
        {
            Driver? driver = _dbContext.Drivers
                                     .Include(d => d.User)
                                     .FirstOrDefault(d => d.UserId == userId);
            return driver;
        }
    }
}
