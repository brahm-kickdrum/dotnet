using Assessment_1.Entities;

namespace Assessment_1.Repositories.IRepositories
{
    public interface IDriverRepository
    {
        public Guid AddDriver(Driver driver);
        public Driver GetDriverByEmail(string email);
    }
}
