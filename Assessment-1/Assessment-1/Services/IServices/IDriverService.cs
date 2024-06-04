using Assessment_1.Entities;

namespace Assessment_1.Services.IServices
{
    public interface IDriverService
    {
        public Guid AddDriver(Driver driver);
        public string LoginDriver(string email, string password);
    }
}
