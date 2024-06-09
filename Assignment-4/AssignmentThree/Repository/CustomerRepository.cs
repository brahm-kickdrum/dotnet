using Assignment_3.DataAccess;
using Assignment_3.Entities;
using Assignment_3.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Assignment_3.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MovieRentalDbContext _dbContext;

        public CustomerRepository(MovieRentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid AddCustomer (Customer customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return customer.CustomerId;
        }

        public bool CustomerExistsByUsername(string username)
        {
            return _dbContext.Customers.Any(c => c.Username == username);
        }

        public Customer? GetCustomerById(Guid id)
        {
            return _dbContext.Customers.Find(id);
        }

        public Customer? GetCustomerByUsername(string username)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.Username == username);
        }
    }
}
