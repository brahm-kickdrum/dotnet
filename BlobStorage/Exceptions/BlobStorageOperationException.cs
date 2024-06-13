namespace BlobStorage.Exceptions
{
    public class BlobStorageOperationException : Exception
    {
        public BlobStorageOperationException(string message) : base(message) { }
    }
}
