using BlobStorage.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlobStorage.Models.Request
{
    public class SetAccessPolicyRequest
    {
        [Required(ErrorMessage = "Container name is required")]
        public string ContainerName { get; set; }

        [Required(ErrorMessage = "Access level is required")]
        public BlobAccessLevel AccessLevel { get; set; }
    }
}
