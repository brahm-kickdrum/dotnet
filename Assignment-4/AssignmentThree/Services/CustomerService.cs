using Assignment_3.DataAccess;
using Assignment_3.Entities;
using Assignment_3.Exceptions;
using Assignment_3.Repository;
using Assignment_3.Repository.IRepository;
using Assignment_3.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Assignment_3.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Guid AddCustomer(Customer customer)
        {
            if (_customerRepository.CustomerExistsByUsername(customer.Username))
            {
                throw new CustomerAlreadyExistsException($"Customer with username '{customer.Username}' already exists.");
            }

            try
            {
                Guid customerId = _customerRepository.AddCustomer(customer);
                return customerId;
            }
            catch (Exception ex)
            {
                throw new FailedToAddCustomerException("Failed to add movie. An error occurred.", ex);
            }

        }

        public Customer GetCustomerById(Guid id)
        {
            Customer? customer = _customerRepository.GetCustomerById(id);

            if (customer == null)
            {
                throw new CustomerNotFoundException("Customer not found.");
            }

            return customer;
        }

        public Customer GetCustomerByUsername(string username)
        {
            Customer? customer = _customerRepository.GetCustomerByUsername(username);

            if (customer == null)
            {
                throw new CustomerNotFoundException("Customer not found.");
            }

            return customer;
        }
    }
}
