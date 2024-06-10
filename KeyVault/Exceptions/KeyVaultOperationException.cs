namespace KeyVault.Exceptions
{
    public class KeyVaultOperationException : Exception
    {
        public KeyVaultOperationException(string message) : base(message) { }
    }
}
