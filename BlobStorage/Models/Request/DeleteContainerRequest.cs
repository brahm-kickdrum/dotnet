using System.ComponentModel.DataAnnotations;

namespace BlobStorage.Models.Request
{
    public class DeleteContainerRequest
    {
        [Required(ErrorMessage = "Container name is required")]
        public string ContainerName { get; set; }
    }
}
