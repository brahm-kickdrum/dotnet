using KeyVault.Models.Requests;

namespace KeyVault.Services.IService
{
    public interface IKeyVaultService
    {
        Task<string> CreateSecretBySecretNameAndValueAsync(SecretCreateRequest secretCreateRequest);

        Task<string> RetrieveSecretBySecretNameAsync(SecretRetrieveRequest secretRetrieveRequest);

        Task<string> DeleteSecretBySecretNameAsync(SecretDeleteRequest secretDeleteRequest);

        Task<string> PurgeSecretBySecretNameAsync(SecretPurgeRequest secretPurgeRequest);
    }
}
