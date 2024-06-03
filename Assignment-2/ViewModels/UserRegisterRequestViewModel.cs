using Assignment_2.Validations;
using System.ComponentModel.DataAnnotations;

namespace Assignment_2.Dto
{
    public class UserRegisterRequestViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public String Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [PasswordValidation]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name must contain only alphabetic characters.")]
        [MinLength(1, ErrorMessage = "Name must not be empty.")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public String Email { get; set; }


        [Required(ErrorMessage = "Address is required")]
        [MinLength(1, ErrorMessage = "Address must not be empty.")]
        public String Address { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]

        public String PhoneNumber { get; set; }

    }
}
