using Homework_7.Enum;
using System.ComponentModel.DataAnnotations;
using static Homework_7.Enum.Gender;

namespace Homework_7.Validations
{
    public class GenderCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value != "")
            {
                if (System.Enum.TryParse(typeof(GenderType), value.ToString(), out object result))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Invalid gender value.");
            }

            return ValidationResult.Success;
        }
    }
}
