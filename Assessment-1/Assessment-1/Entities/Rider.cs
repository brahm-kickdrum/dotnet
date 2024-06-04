using Assessment_1.Validations;
using System.ComponentModel.DataAnnotations;

namespace Assessment_1.Entities
{
    public class Rider
    {
        [Required]
        public Guid Id { get; set; }
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
        public DateTime CreatedAt { get; set; }

        public Rider(string name, string email, string phone, string password)
        {
            Id = Guid.NewGuid(); 
            Name = name;
            Email = email;
            Phone = phone;
            Password = password;
            CreatedAt = DateTime.UtcNow; 
        }
    }
}
