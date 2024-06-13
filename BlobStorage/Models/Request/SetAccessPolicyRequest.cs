using BlobStorage.Constants;
using BlobStorage.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlobStorage.Models.Request
{
    public class SetAccessPolicyRequest
    {
        [Required]
        [RegularExpression(AppConstants.ContainerNameRegex, ErrorMessage = ErrorMessages.ContainerNameError)]
        [StringLength(63, MinimumLength = 3, ErrorMessage = ErrorMessages.ContainerNameLengthError)]
        public string ContainerName { get; set; }

        [Required]
        public BlobAccessLevel AccessLevel { get; set; }
    }
}
