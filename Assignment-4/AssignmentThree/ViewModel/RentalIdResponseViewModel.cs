namespace Assignment_3.Dtos
{
    public class RentalIdResponseViewModel
    {
        public Guid RentalId { get; set; }

        public RentalIdResponseViewModel(Guid id)
        {
            RentalId = id;
        }
    }
}
