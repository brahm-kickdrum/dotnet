using Homework_7.Validations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Homework_7.Dto
{
    public class UserRequestDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 120, ErrorMessage = "Age must be between 18 and 120.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }
  
        [CountryCheck]
        public string? Country { get; set; }

        [GenderCheck]
        public string? Gender { get; set; }

        [RegularExpression(@"^\d{4} \d{4} \d{4} \d{4}$", ErrorMessage = "Invalid credit card number format.")]
        public string? CreditCardNumber { get; set; }

        [DateCheck]
        public string? ExpirationDate { get; set; }

        [Range(100, 9999, ErrorMessage = "CVV must be a 3 or 4-digit number.")]
        public int? CVV { get; set; }
    }
}
