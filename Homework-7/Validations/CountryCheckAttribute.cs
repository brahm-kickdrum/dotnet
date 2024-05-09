
using System.ComponentModel.DataAnnotations;
using static Homework_7.Enum.Country;

namespace Homework_7.Validations
{
    public class CountryCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != "" &&value != null)
            {
                if (System.Enum.TryParse(typeof(CountryType), value.ToString(), out object result))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Invalid country value.");
            }

            return ValidationResult.Success;
        }
    }
}
