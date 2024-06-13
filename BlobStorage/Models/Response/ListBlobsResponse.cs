namespace BlobStorage.Models.Response
{
    public class ListBlobsResponse
    {
        public List<string> blobs { get; set; }

        public ListBlobsResponse( List<string> blobs )
        {
            this.blobs = blobs;
        }
    }
}
