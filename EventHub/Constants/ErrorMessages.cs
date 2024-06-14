namespace EventHub.Constants
{
    public static class ErrorMessages
    {
        public const string ConfigurationError = "Oops! It looks like something went wrong while trying to connect.";
        public const string SecretNotFoundError = "Secret not found";
        public const string KeyVaultNameError = "Please set the KEY_VAULT_NAME environment variable.";
        public const string ErrorProcessingEvent = "Error processing event from partition {0}";
        public const string ServerError = "Server error";
    }
}
