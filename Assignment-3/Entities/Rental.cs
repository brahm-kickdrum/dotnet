using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_3.Entities
{
    public class Rental
    {
        [Key]
        public Guid RentalId { get; set; }

        [Required(ErrorMessage = "Rental date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        public DateTime RentalDate { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        public DateTime? ReturnDate { get; set; }

        [Required(ErrorMessage = "Movie Id is required.")]
        public Guid MovieId { get; set; }

        [Required(ErrorMessage = "Customer Id is required.")]
        public Guid CustomerId { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public Rental(Guid movieId, Guid customerId)
        {
            RentalId = Guid.NewGuid();
            RentalDate = DateTime.UtcNow;
            ReturnDate = null;
            MovieId = movieId;
            CustomerId = customerId;
        }
    }
}
