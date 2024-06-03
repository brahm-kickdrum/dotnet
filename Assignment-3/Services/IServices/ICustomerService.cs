using Assignment_3.Entities;

namespace Assignment_3.Services.IServices
{
    public interface ICustomerService
    {
        Guid AddCustomer(Customer customer);
    }
}
