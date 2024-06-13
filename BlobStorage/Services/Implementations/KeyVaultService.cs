using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using BlobStorage.Exceptions;
using BlobStorage.Services.IServices;

namespace BlobStorage.Services.Implementations
{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly SecretClient _secretClient;

        public KeyVaultService()
        {
            string? keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME");

            if (string.IsNullOrEmpty(keyVaultName))
            {
                throw new ConfigurationException("Please set the KEY_VAULT_NAME environment variable.");
            }

            string kvUri = $"https://{keyVaultName}.vault.azure.net";
            _secretClient = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        }

        public async Task<string> RetrieveSecretAsync(string secretName)
        {
            try
            {
                KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);
                return secret.Value;
            }
            catch (Exception)
            {
                throw new KeyVaultOperationException("Secret not found");
            }
        }
    }
}
