using Assessment_1.Entities;

namespace Assessment_1.Repositories.IRepositories
{
    public interface IRiderRepository
    {
        public Guid AddRider(Rider rider);
        public Rider GetRiderByEmail(string email);
    }
}
