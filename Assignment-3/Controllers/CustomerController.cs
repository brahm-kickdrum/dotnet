using Assignment_3.Dtos;
using Assignment_3.Exceptions;
using Assignment_3.Mappers;
using Assignment_3.Services;
using Assignment_3.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_3.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public ActionResult<CustomerIdResponseViewModel> Post([FromBody] CreateCustomerRequestViewModel createCustomerRequestViewModel)
        {
            Guid customerId = _customerService.AddCustomer(CustomerMapper.CreateCustomerFromViewModel(createCustomerRequestViewModel));
            return Ok(CustomerMapper.CreateResponseViewModelFromCustomerId(customerId));
        }
    }
}
