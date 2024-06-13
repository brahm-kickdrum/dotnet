using System.ComponentModel.DataAnnotations;

namespace BlobStorage.Models.Request
{
    public class CreateContainerRequest
    {
        [RegularExpression(@"^[a-z0-9]([a-z0-9]*-?[a-z0-9]+)*$", ErrorMessage = "Container name must only contain lowercase letters, numbers and hyphens and must begin with a letter or number. Each hyphen must be preceded and followed by a non-hyphen character.")]
        [StringLength(63, MinimumLength = 3, ErrorMessage = "Container name must be between 3 and 63 characters long.")]
        public string ContainerName { get; set; }
    }
}
