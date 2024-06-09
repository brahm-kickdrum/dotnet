using Assessment_1.Enums;
using Assessment_1.Validations;
using System.ComponentModel.DataAnnotations;

namespace Assessment_1.Models
{
    public class DriverRegisterRequest
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 50 characters.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "First name can only contain letters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 50 characters.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Last name can only contain letters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [PasswordValidation]
        public string Password { get; set; }

        [Required(ErrorMessage = "Plate number is required.")]
        [StringLength(15, ErrorMessage = "Plate number cannot be longer than 15 characters.")]
        [RegularExpression("^[A-Z0-9-]+$", ErrorMessage = "Plate number can only contain uppercase letters, numbers, and hyphens.")]
        public string PlateNumber { get; set; }

        [Required(ErrorMessage = "Vehicle type is required.")]
        public VehicleType VehicleType { get; set; }

        [Required(ErrorMessage = "License number is required.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "License number must be exactly 16 digits.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "License number must contain only digits.")]
        public string LicenseNumber { get; set; }
    }
}
