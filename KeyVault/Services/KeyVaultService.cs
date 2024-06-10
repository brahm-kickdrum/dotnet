using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using KeyVault.Exceptions;
using KeyVault.Models;
using KeyVault.Services.IService;

namespace KeyVault.Services
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

        public async Task<string> CreateSecretAsync(SecretCreateRequest secretCreateRequest)
        {
            try
            {
                KeyVaultSecret secret = new KeyVaultSecret(secretCreateRequest.SecretName, secretCreateRequest.SecretValue);
                await _secretClient.SetSecretAsync(secret);
                return $"Secret '{secretCreateRequest.SecretName}' created successfully.";
            }
            catch (Exception)
            {
                throw new KeyVaultOperationException("Error creating secret");
            }
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

        public async Task<string> DeleteSecretAsync(string secretName)
        {
            try
            {
                DeleteSecretOperation operation = await _secretClient.StartDeleteSecretAsync(secretName);
                await operation.WaitForCompletionAsync();
                return $"Secret '{secretName}' deleted successfully.";
            }
            catch (Exception)
            {
                throw new KeyVaultOperationException("Error deleting secret");
            }
        }

        public async Task<string> PurgeSecretAsync(string secretName)
        {
            try
            {
                await _secretClient.PurgeDeletedSecretAsync(secretName);
                return $"Secret '{secretName}' purged successfully.";
            }
            catch (RequestFailedException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw new KeyVaultOperationException("Error purging secret");
            }
        }
    }
}
