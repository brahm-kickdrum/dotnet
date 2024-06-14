namespace KeyVault.Constants
{
    public static class ErrorMessages
    {
        public const string KeyVaultNameNotSet = "Please set the KEY_VAULT_NAME environment variable.";
        public const string SecretCreationError = "Error creating secret";
        public const string SecretNotFoundError = "Secret not found";
        public const string SecretDeletionError = "Error deleting secret";
        public const string SecretPurgeError = "Error purging secret";
        public const string ServerError = "Server error";
        public const string InvalidSecretName = "Please provide a valid secret name. Secret names can only contain alphanumeric characters and dashes.";
        public const string SecretNameLength = "Secret name must be between 1 and 127 characters.";
        public const string SecretValueLength = "Secret value must be between 1 and 25,600 characters.";
    }
}
