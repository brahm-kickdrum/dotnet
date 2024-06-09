using Assignment_3.Entities;

namespace Assignment_3.Repository.IRepository
{
    public interface ICustomerRepository
    {
        Guid AddCustomer(Customer customer);

        bool CustomerExistsByUsername(string username);

        Customer? GetCustomerById(Guid id);

        public Customer? GetCustomerByUsername(string username);
    }
}
