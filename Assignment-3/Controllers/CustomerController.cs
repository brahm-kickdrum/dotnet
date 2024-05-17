using Assignment_3.Dtos;
using Assignment_3.Exceptions;
using Assignment_3.Mappers;
using Assignment_3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_3.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public ActionResult<CustomerIdResponseDto> Post([FromBody] CreateCustomerRequestDto createCustomerRequestDto)
        {
            try
            {
                Guid customerId = _customerService.AddCustomer(CustomerMapper.CreateCustomerFromDto(createCustomerRequestDto));
                return Ok(CustomerMapper.CreateResponseDtoFromCustomerId(customerId));
            }
            catch (FailedToAddCustomerException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (CustomerAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
