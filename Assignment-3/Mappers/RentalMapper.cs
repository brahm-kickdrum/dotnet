using Assignment_3.Dtos;

namespace Assignment_3.Mappers
{
    public static class RentalMapper
    {
        public static RentalIdResponseViewModel CreateRentalIdResponseViewModelFromRentalId(Guid rentalId)
        {
            return new RentalIdResponseViewModel(rentalId);
        }
    }
}
