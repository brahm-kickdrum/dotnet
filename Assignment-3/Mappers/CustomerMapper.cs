using Assignment_3.Dtos;
using Assignment_3.Entities;

namespace Assignment_3.Mappers
{
    public static class CustomerMapper
    {
        public static Customer CreateCustomerFromViewModel(CreateCustomerRequestViewModel viewModel)
        {
            return new Customer(viewModel.Username, viewModel.Name, viewModel.Email);
        }

        public static CustomerIdResponseViewModel CreateResponseViewModelFromCustomerId(Guid customerId)
        {
            return new CustomerIdResponseViewModel(customerId);
        }
    }
}
