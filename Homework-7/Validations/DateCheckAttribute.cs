using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Homework_7.Validations
{
    public class DateCheckAttribute : ValidationAttribute   
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (DateTime.TryParseExact((string?)value, "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                DateTime currentDate = DateTime.Now;

                if (parsedDate < currentDate)
                {
                    return new ValidationResult("Date must be greater than current date");
                }
                return ValidationResult.Success;
            }
            else if(value == "" || value == null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Enter the date in correct format i.e. MM/YYYY");
        }
    }
}
