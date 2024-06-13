using System.ComponentModel.DataAnnotations;

namespace BlobStorage.Models.Request
{
    public class DeleteBlobRequest
    {
        [Required(ErrorMessage = "Container name is required")]
        public string ContainerName { get; set; }

        [Required(ErrorMessage = "Blob name is required")]
        public string BlobName { get; set; }
    }
}
