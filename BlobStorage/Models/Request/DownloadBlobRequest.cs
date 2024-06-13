using System.ComponentModel.DataAnnotations;

namespace BlobStorage.Models.Request
{
    public class DownloadBlobRequest
    {
        [Required(ErrorMessage = "Container name is required")]
        public string ContainerName { get; set; }

        [Required(ErrorMessage = "Blob name is required")]
        public string BlobName { get; set; }

        [Required(ErrorMessage = "Local file path is required")]
        public string LocalFilePath { get; set; }
    }
}
