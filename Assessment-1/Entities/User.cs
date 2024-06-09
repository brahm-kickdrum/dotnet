using Assessment_1.Enums;
using System.ComponentModel.DataAnnotations;

namespace Assessment_1.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public User(string firstName, string lastName, string email, string phone, string password, UserRole role)
        {
            UserId = new Guid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Password = password;
            Role = role;
        }

        public User() { }
    }
}
