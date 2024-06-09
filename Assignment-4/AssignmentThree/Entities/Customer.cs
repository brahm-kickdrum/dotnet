using System.ComponentModel.DataAnnotations;

namespace Assignment_3.Entities
{
    public class Customer
    {
        [Key]
        [Required]
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username length can't be more than 50.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(50, ErrorMessage = "Email length can't be more than 50.")]
        public string Email { get; set; }

        public Customer(string username, string name, string email)
        {
            CustomerId = Guid.NewGuid();
            Username = username;
            Name = name;
            Email = email;
        }
    }
}
