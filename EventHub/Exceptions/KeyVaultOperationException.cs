namespace EventHub.Exceptions
{
    public class KeyVaultOperationException : Exception
    {
        public KeyVaultOperationException(string message) : base(message) { }
    }
}
