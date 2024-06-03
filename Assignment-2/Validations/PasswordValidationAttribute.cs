using System.ComponentModel.DataAnnotations;

namespace Assignment_2.Validations
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? password = value?.ToString();

            if (password == null)
                return new ValidationResult("Password cannot be null.");
            
            if (password.Length < 8 || password.Length > 20)
                return new ValidationResult("Password must be between 8 and 20 characters long.");

            if (!password.Any(char.IsUpper))
                return new ValidationResult("Password must contain at least one uppercase letter.");

            if (!password.Any(char.IsLower))
                return new ValidationResult("Password must contain at least one lowercase letter.");

            if (!password.Any(char.IsDigit))
                return new ValidationResult("Password must contain at least one digit.");

            char[] specialCharacters = new[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '+', '=', '{', '}', '[', ']', '|', '\\', ';', ':', '\'', '"', '<', '>', ',', '.', '/', '?' };
            if (!password.Any(c => specialCharacters.Contains(c)))
                return new ValidationResult("Password must contain at least one special character.");

            return ValidationResult.Success;
        }
    }
}
