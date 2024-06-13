namespace BlobStorage.Exceptions
{
    public class KeyVaultOperationException : Exception
    {
        public KeyVaultOperationException(string message) : base(message) { }
    }
}
