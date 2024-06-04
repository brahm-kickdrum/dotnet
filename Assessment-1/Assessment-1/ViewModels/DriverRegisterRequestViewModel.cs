using Assessment_1.Validations;
using System.ComponentModel.DataAnnotations;

namespace Assessment_1.ViewModels
{
    public class DriverRegisterRequestViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name must contain only alphabetic characters.")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [PasswordValidation]
        public string Password { get; set; }
        [Required]
        public string VehicleNumber { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "Licence Number must be 16 digits")]
        [RegularExpression(@"^[1-9]+$", ErrorMessage = "Name must contain only alphabetic characters.")]
        public string LicenceNumber { get; set; }

        public DriverRegisterRequestViewModel(string name, string email, string phone, string password, string vehicleNumber, string licenceNumber)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Password = password;
            VehicleNumber = vehicleNumber;
            LicenceNumber = licenceNumber;
        }
    }
}
