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
            CreateCustomerRequestDto customerRequestDto = new CreateCustomerRequestDto
            {
                Username = "testuser",
                Name = "Test User",
                Email = "testuser@example.com"
            };
            Guid customerId = Guid.NewGuid();

            _mockCustomerService.Setup(service => service.AddCustomer(It.IsAny<Customer>()))
                .Returns(customerId);

            // Act
            ActionResult<CustomerIdResponseDto> result = _controller.Post(customerRequestDto);

            // Assert
            ActionResult<CustomerIdResponseDto> actionResult = Assert.IsType<ActionResult<CustomerIdResponseDto>>(result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            CustomerIdResponseDto responseDto = Assert.IsType<CustomerIdResponseDto>(okResult.Value);
            Assert.Equal(customerId, responseDto.CustomerId);
        }

        [Fact]
        public void Post_ReturnsBadRequest_WhenCustomerAlreadyExists()
        {
            // Arrange
            CreateCustomerRequestDto customerRequestDto = new CreateCustomerRequestDto
            {
                Username = "existinguser",
                Name = "Existing User",
                Email = "existinguser@example.com"
            };

            _mockCustomerService.Setup(service => service.AddCustomer(It.IsAny<Customer>()))
                .Throws(new CustomerAlreadyExistsException("Customer already exists"));

            // Act
            ActionResult<CustomerIdResponseDto> result = _controller.Post(customerRequestDto);

            // Assert
            ActionResult<CustomerIdResponseDto> actionResult = Assert.IsType<ActionResult<CustomerIdResponseDto>>(result);
            BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Customer already exists", badRequestResult.Value);
        }

        [Fact]
        public void Post_ReturnsInternalServerError_WhenFailedToAddCustomer()
        {
            // Arrange
            CreateCustomerRequestDto customerRequestDto = new CreateCustomerRequestDto
            {
                Username = "newuser",
                Name = "New User",
                Email = "newuser@example.com"
            };

            _mockCustomerService.Setup(service => service.AddCustomer(It.IsAny<Customer>()))
                .Throws(new FailedToAddCustomerException("Failed to add customer"));

            // Act
            ActionResult<CustomerIdResponseDto> result = _controller.Post(customerRequestDto);

            // Assert
            ActionResult<CustomerIdResponseDto> actionResult = Assert.IsType<ActionResult<CustomerIdResponseDto>>(result);
            ObjectResult statusCodeResult = Assert.IsType<ObjectResult>(actionResult.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Failed to add customer", statusCodeResult.Value);
        }
    }
}
