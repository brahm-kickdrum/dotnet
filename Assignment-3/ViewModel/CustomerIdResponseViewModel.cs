namespace Assignment_3.Dtos
{
    public class CustomerIdResponseViewModel
    {
        public Guid CustomerId { get; set; }

        public CustomerIdResponseViewModel(Guid id)
        {
            CustomerId = id;
        }
    }
}
