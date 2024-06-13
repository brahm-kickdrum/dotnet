namespace BlobStorage.Services.IServices
{
    public interface IKeyVaultService
    {
        Task<string> RetrieveSecretAsync(string secretName);
    }
}
