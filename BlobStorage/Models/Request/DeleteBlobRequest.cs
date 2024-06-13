using BlobStorage.Constants;
using System.ComponentModel.DataAnnotations;

namespace BlobStorage.Models.Request
{
    public class DeleteBlobRequest
    {
        [Required]
        [RegularExpression(AppConstants.ContainerNameRegex, ErrorMessage = ErrorMessages.ContainerNameError)]
        [StringLength(63, MinimumLength = 3, ErrorMessage = ErrorMessages.ContainerNameLengthError)]
        public string ContainerName { get; set; }

        [Required]
        public string BlobName { get; set; }
    }
}
