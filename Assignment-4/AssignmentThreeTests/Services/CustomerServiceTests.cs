using Assignment_3.DataAccess;
using Assignment_3.Entities;
using Assignment_3.Exceptions;
using Assignment_3.Repository.IRepository;
using Assignment_3.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentThreeTests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_mockCustomerRepository.Object);
        }

        [Fact]
        public void AddCustomer_CustomerExists_ThrowsCustomerAlreadyExistsException()
        {
            // Arrange
            Customer customer = new Customer("existingUser", "TestCustomer", "testcustomer@example.com");
            _mockCustomerRepository.Setup(repo => repo.CustomerExistsByUsername(customer.Username)).Returns(true);

            // Act & Assert
            Assert.Throws<CustomerAlreadyExistsException>(() => _customerService.AddCustomer(customer));
        }

        [Fact]
        public void AddCustomer_ValidCustomer_ReturnsCustomerId()
        {
            // Arrange
            Customer customer = new Customer("newUser", "TestCustomer", "testcustomer@example.com");
            Guid customerId = customer.CustomerId;
            _mockCustomerRepository.Setup(repo => repo.CustomerExistsByUsername(customer.Username)).Returns(false);
            _mockCustomerRepository.Setup(repo => repo.AddCustomer(customer)).Returns(customerId);

            // Act
            Guid result = _customerService.AddCustomer(customer);

            // Assert
            Assert.Equal(customerId, result);
        }

        [Fact]
        public void GetCustomerById_CustomerNotFound_ThrowsCustomerNotFoundException()
        {
            // Arrange
            Guid customerId = Guid.NewGuid();
            _mockCustomerRepository.Setup(repo => repo.GetCustomerById(customerId)).Returns((Customer)null);

            // Act & Assert
            Assert.Throws<CustomerNotFoundException>(() => _customerService.GetCustomerById(customerId));
        }

        [Fact]
        public void GetCustomerById_CustomerFound_ReturnsCustomer()
        {
            // Arrange
            Guid customerId = Guid.NewGuid();
            Customer customer = new Customer("user123", "TestCustomer", "testcustomer@example.com") { CustomerId = customerId };
            _mockCustomerRepository.Setup(repo => repo.GetCustomerById(customerId)).Returns(customer);

            // Act
            Customer result = _customerService.GetCustomerById(customerId);

            // Assert
            Assert.Equal(customer, result);
        }

        [Fact]
        public void GetCustomerByUsername_CustomerNotFound_ThrowsCustomerNotFoundException()
        {
            // Arrange
            string username = "nonexistentUser";
            _mockCustomerRepository.Setup(repo => repo.GetCustomerByUsername(username)).Returns((Customer)null);

            // Act & Assert
            Assert.Throws<CustomerNotFoundException>(() => _customerService.GetCustomerByUsername(username));
        }

        [Fact]
        public void GetCustomerByUsername_CustomerFound_ReturnsCustomer()
        {
            // Arrange
            string username = "existingUser";
            Customer customer = new Customer(username, "TestCustomer", "testcustomer@example.com");
            _mockCustomerRepository.Setup(repo => repo.GetCustomerByUsername(username)).Returns(customer);

            // Act
            Customer result = _customerService.GetCustomerByUsername(username);

            // Assert
            Assert.Equal(customer, result);
        }
    }
}
