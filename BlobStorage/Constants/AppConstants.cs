namespace BlobStorage.Constants
{
    public static class AppConstants
    {
        public const string ContainerNameRegex = @"^[a-z0-9]([a-z0-9]*-?[a-z0-9]+)*$";
        public const string BlobStorageConnectionString = "BlobStorageConnectionString";
        public const string KeyVaultName = "KEY_VAULT_NAME";
        public const string KeyVaultUri = "https://{0}.vault.azure.net";
        public const int ContainerNameMaxLength = 63;
        public const int ContainerNameMinLength = 3;
    }
}
