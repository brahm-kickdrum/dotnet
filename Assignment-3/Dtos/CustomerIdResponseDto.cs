namespace Assignment_3.Dtos
{
    public class CustomerIdResponseDto
    {
        public Guid CustomerId { get; set; }

        public CustomerIdResponseDto(Guid id)
        {
            CustomerId = id;
        }
    }
}
