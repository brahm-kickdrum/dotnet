using KeyVault.Constants;
using System.ComponentModel.DataAnnotations;

namespace KeyVault.Models.Requests
{
    public class SecretCreateRequest
    {
        [Required]
        [RegularExpression(AppConstants.SecretNameRegex, ErrorMessage = ErrorMessages.InvalidSecretName)]
        [StringLength(AppConstants.SecretNameMaxLength, MinimumLength = AppConstants.SecretNameMinLength, ErrorMessage = ErrorMessages.SecretNameLength)]
        public string SecretName { get; set; }

        [Required]
        [StringLength(AppConstants.SecretValueMaxLength, MinimumLength = AppConstants.SecretValueMinLength, ErrorMessage = ErrorMessages.SecretValueLength)]
        public string SecretValue { get; set; }

    }
}
