namespace BlobStorage.Configurations
{
    public class BlobStorageConfigurations
    {
        public string BlobStorageConnectionString { get; set; }

        public BlobStorageConfigurations(string blobStorageConnectionString) 
        {
            BlobStorageConnectionString = blobStorageConnectionString;
        }
    }
}
