using System.ComponentModel.DataAnnotations;

namespace BlobStorage.Models.Request
{
    public class UploadFileRequest
    {
        [Required(ErrorMessage = "Container name is required")]
        public string ContainerName { get; set; }

        [Required(ErrorMessage = "Local file path is required")]
        public string LocalFilePath { get; set; }

        [Required(ErrorMessage = "File name is required")]
        public string FileName { get; set; }
    }
}
