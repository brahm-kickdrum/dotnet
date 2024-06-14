namespace KeyVault.Constants
{
    public static class AppConstants
    {
        public const string SecretNameRegex = @"^[a-zA-Z0-9-]+$";
        public const string KeyVaultName = "KEY_VAULT_NAME";
        public const string KeyVaultUri = "https://{0}.vault.azure.net";
        public const int SecretNameMaxLength = 127;
        public const int SecretNameMinLength = 1;
        public const int SecretValueMaxLength = 25600;
        public const int SecretValueMinLength = 1;
    }
}
