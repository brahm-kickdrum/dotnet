using KeyVault.Models;

namespace KeyVault.Services.IService
{
    public interface IKeyVaultService
    {
        Task<string> CreateSecretAsync(SecretCreateRequest secretCreateRequest);

        Task<string> RetrieveSecretAsync(string secretName);

        Task<string> DeleteSecretAsync(string secretName);

        Task<string> PurgeSecretAsync(string secretName);
    }
}
