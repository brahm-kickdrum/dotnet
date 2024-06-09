using Assignment_3.Controllers;
using Assignment_3.Dtos;
using Assignment_3.Entities;
using Assignment_3.Exceptions;
using Assignment_3.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentThreeTests.Contollers
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _controller = new CustomerController(_mockCustomerService.Object);
        }

        [Fact]
        public void Post_ReturnsOkResult_WhenCustomerIsAddedSuccessfully()
        {
            // Arrange
            CreateCustomerRequestViewModel customerRequestViewModel = new CreateCustomerRequestViewModel
            {
                Username = "testuser",
                Name = "Test User",
                Email = "testuser@example.com"
            };
            Guid customerId = Guid.NewGuid();

            _mockCustomerService.Setup(service => service.AddCustomer(It.IsAny<Customer>()))
                .Returns(customerId);

            // Act
            ActionResult<CustomerIdResponseViewModel> result = _controller.Post(customerRequestViewModel);

            // Assert
            ActionResult<CustomerIdResponseViewModel> actionResult = Assert.IsType<ActionResult<CustomerIdResponseViewModel>>(result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            CustomerIdResponseViewModel responseViewModel = Assert.IsType<CustomerIdResponseViewModel>(okResult.Value);
            Assert.Equal(customerId, responseViewModel.CustomerId);
        }
    }
}
