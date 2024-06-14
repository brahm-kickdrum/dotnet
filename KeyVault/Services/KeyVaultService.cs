using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using KeyVault.Constants;
using KeyVault.Exceptions;
using KeyVault.Models.Requests;
using KeyVault.Services.IService;

namespace KeyVault.Services
{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly SecretClient _secretClient;

        public KeyVaultService()
        {
            string? keyVaultName = Environment.GetEnvironmentVariable(AppConstants.KeyVaultName);

            if (string.IsNullOrEmpty(keyVaultName))
            {
                throw new ConfigurationException(ErrorMessages.KeyVaultNameNotSet);
            }

            string kvUri = string.Format(AppConstants.KeyVaultUri, keyVaultName);
            _secretClient = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        }

        public async Task<string> CreateSecretBySecretNameAndValueAsync(SecretCreateRequest secretCreateRequest)
        {
            try
            {
                KeyVaultSecret secret = new KeyVaultSecret(secretCreateRequest.SecretName, secretCreateRequest.SecretValue);
                await _secretClient.SetSecretAsync(secret);
                return string.Format(ResponseMessages.SecretCreated, secretCreateRequest.SecretName);
            }
            catch (Exception)
            {
                throw new KeyVaultOperationException(ErrorMessages.SecretCreationError);
            }
        }

        public async Task<string> RetrieveSecretBySecretNameAsync(SecretRetrieveRequest secretRetrieveRequest)
        {
            try
            {
                KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretRetrieveRequest.SecretName);
                return secret.Value;
            }
            catch (Exception)
            {
                throw new KeyVaultOperationException(ErrorMessages.SecretNotFoundError);
            }
        }

        public async Task<string> DeleteSecretBySecretNameAsync(SecretDeleteRequest secretDeleteRequest)
        {
            try
            {
                DeleteSecretOperation operation = await _secretClient.StartDeleteSecretAsync(secretDeleteRequest.SecretName);
                await operation.WaitForCompletionAsync();
                return string.Format(ResponseMessages.SecretDeleted, secretDeleteRequest.SecretName);
            }
            catch (Exception)
            {
                throw new KeyVaultOperationException(ErrorMessages.SecretDeletionError);
            }
        }

        public async Task<string> PurgeSecretBySecretNameAsync(SecretPurgeRequest secretPurgeRequest)
        {
            try
            {
                await _secretClient.PurgeDeletedSecretAsync(secretPurgeRequest.SecretName);
                return string.Format(ResponseMessages.SecretPurged, secretPurgeRequest.SecretName);
            }
            catch (RequestFailedException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw new KeyVaultOperationException(ErrorMessages.SecretPurgeError);
            }
        }
    }
}
