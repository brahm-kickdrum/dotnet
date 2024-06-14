using KeyVault.Constants;
using System.ComponentModel.DataAnnotations;

namespace KeyVault.Models.Requests
{
    public class SecretDeleteRequest
    {
        [Required]
        [RegularExpression(AppConstants.SecretNameRegex, ErrorMessage = ErrorMessages.InvalidSecretName)]
        [StringLength(AppConstants.SecretNameMaxLength, MinimumLength = AppConstants.SecretNameMinLength, ErrorMessage = ErrorMessages.SecretNameLength)]
        public string SecretName { get; set; }
    }
}
