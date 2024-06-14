using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using BlobStorage.Constants;
using BlobStorage.Exceptions;
using BlobStorage.Services.IServices;

namespace BlobStorage.Services.Implementations
{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly SecretClient _secretClient;

        public KeyVaultService()
        {
            string? keyVaultName = Environment.GetEnvironmentVariable(AppConstants.KeyVaultName);

            if (string.IsNullOrEmpty(keyVaultName))
            {
                throw new ConfigurationException(ErrorMessages.EnvironmentVariableError);
            }

            string kvUri = string.Format(AppConstants.KeyVaultUri, keyVaultName);
            _secretClient = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        }

        public async Task<string> RetrieveSecretAsync(string secretName)
        {
            try
            {
                await Console.Out.WriteLineAsync("hellooooo");
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
