namespace EventHub.Configurations
{
    public class BlobStorageConfigurations
    {
        public string BlobStorageConnectionString { get; set; }
        public string BlobStorageContainerName { get; set; }

        public BlobStorageConfigurations(string blobStorageConnectionString, string blobStorageContainerName)
        {
            BlobStorageConnectionString = blobStorageConnectionString;
            BlobStorageContainerName = blobStorageContainerName;
        }
    }
}
