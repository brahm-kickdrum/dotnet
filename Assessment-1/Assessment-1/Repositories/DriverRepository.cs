using Assessment_1.DataAccess;
using Assessment_1.Entities;
using Assessment_1.Exceptions;
using Assessment_1.Repositories.IRepositories;

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
            if (_dbContext.Drivers.Any(d => d.Email == driver.Email || _dbContext.Drivers.Any(d => d.Phone == driver.Phone)))
            {
                throw new DriverAlreadyExistsException("Driver with email or phone already exists");
            }

            _dbContext.Drivers.Add(driver);
            _dbContext.SaveChanges();

            return driver.Id;
        }

        public Driver GetDriverByEmail(string email)
        {
            Driver? driver = _dbContext.Drivers.FirstOrDefault(d => d.Email == email);
            if (driver == null)
            {
                throw new DriverNotFoundException("Driver not found");
            }
            return driver;
        }
    }
}
