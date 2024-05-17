using System.ComponentModel.DataAnnotations;

namespace Assignment_3.Dtos
{
    public class CreateCustomerRequestDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username length can't be more than 50.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(100, ErrorMessage = "Email length can't be more than 100.")]
        public string Email { get; set; }
    }
}
