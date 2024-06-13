using BlobStorage.Constants;
using System.ComponentModel.DataAnnotations;

namespace BlobStorage.Models.Request
{
    public class UploadFileRequest
    {
        [Required]
        [RegularExpression(AppConstants.ContainerNameRegex, ErrorMessage = ErrorMessages.ContainerNameError)]
        [StringLength(63, MinimumLength = 3, ErrorMessage = ErrorMessages.ContainerNameLengthError)]
        public string ContainerName { get; set; }

        [Required]
        public string LocalFilePath { get; set; }

        [Required]
        public string FileName { get; set; }
    }
}
