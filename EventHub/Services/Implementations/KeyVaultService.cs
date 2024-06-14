using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using EventHub.Constants;
using EventHub.Exceptions;
using EventHub.Services.IServices;

namespace EventHub.Services.Implementations
{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly SecretClient _secretClient;

        public KeyVaultService()
        {
            string? keyVaultName = Environment.GetEnvironmentVariable(AppConstants.KeyVaultName);

            if (string.IsNullOrEmpty(keyVaultName))
            {
                throw new ConfigurationException(ErrorMessages.KeyVaultNameError);
            }

            string kvUri = string.Format(AppConstants.KeyVaultUri, keyVaultName);
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
                throw new KeyVaultOperationException(ErrorMessages.SecretNotFoundError);
            }
        }
    }
}
