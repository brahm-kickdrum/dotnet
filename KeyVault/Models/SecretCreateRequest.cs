using System.ComponentModel.DataAnnotations;

namespace KeyVault.Models
{
    public class SecretCreateRequest
    {
        [Required(ErrorMessage = "Secret name is required.")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "Please provide a valid secret name. Secret names can only contain alphanumeric characters and dashes.")]
        [StringLength(127, MinimumLength = 1, ErrorMessage = "Secret name must be between 1 and 127 characters.")]
        public string SecretName { get; set; }

        [Required(ErrorMessage = "Secret value is required.")]
        [StringLength(25 * 1024, MinimumLength = 1, ErrorMessage = "Secret value must be between 1 and 200 characters.")]
        public string SecretValue { get; set; }

    }
}
