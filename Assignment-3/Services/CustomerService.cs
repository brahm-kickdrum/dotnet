using Assignment_3.DataAccess;
using Assignment_3.Entities;
using Assignment_3.Exceptions;
using Assignment_3.Services.IServices;

namespace Assignment_3.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly MovieRentalDbContext _dbContext;

        public CustomerService(MovieRentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid AddCustomer(Customer customer)
        {
            if (_dbContext.Customers.Any(c => c.Username == customer.Username))
            {
                throw new CustomerAlreadyExistsException($"Customer with username '{customer.Username}' already exists.");
            }

            try
            {
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();

                return customer.CustomerId;
            }
            catch (Exception ex)
            {
                throw new FailedToAddCustomerException("Failed to add movie. An error occurred.", ex);
            }

        }
    }
}
