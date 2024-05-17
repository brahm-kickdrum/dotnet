using Assignment_3.Dtos;
using Assignment_3.Entities;

namespace Assignment_3.Mappers
{
    public static class CustomerMapper
    {
        public static Customer CreateCustomerFromDto(CreateCustomerRequestDto dto)
        {
            return new Customer(dto.Username, dto.Name, dto.Email);
        }

        public static CustomerIdResponseDto CreateResponseDtoFromCustomerId(Guid customerId)
        {
            return new CustomerIdResponseDto(customerId);
        }
    }
}
