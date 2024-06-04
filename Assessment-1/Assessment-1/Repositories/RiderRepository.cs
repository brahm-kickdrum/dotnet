using Assessment_1.DataAccess;
using Assessment_1.Entities;
using Assessment_1.Exceptions;
using Assessment_1.Repositories.IRepositories;

namespace Assessment_1.Repositories
{
    public class RiderRepository : IRiderRepository
    {
        private readonly RideDbContext _dbContext;

        public RiderRepository(RideDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid AddRider(Rider rider)
        {
            if(_dbContext.Riders.Any(r => r.Email == rider.Email || _dbContext.Drivers.Any(r => r.Phone == rider.Phone)))
            {
                throw new RiderAlreadyExistsException("Rider with email or phone already exists");
            }
            
            _dbContext.Riders.Add(rider);
            _dbContext.SaveChanges();

            return rider.Id;
        }

        public Rider GetRiderByEmail(string email)
        {
            Rider? rider  = _dbContext.Riders.FirstOrDefault(r => r.Email == email);
            if(rider == null)
            {
                throw new RiderNotFoundException("Rider not found");
            }
            return rider;  
        }
    }
}
