using BlobStorage.Models.Request;
using BlobStorage.Models.Response;

namespace BlobStorage.Services.IServices
{
    public interface IBlobStorageService
    {
        Task<string> CreateContainerByContainerNameAsync(CreateContainerRequest createContainerRequest);

        Task<string> SetAccessPolicyByContainerNameAsync(SetAccessPolicyRequest setAccessPolicyRequest);

        Task<string> UploadFileToContainerAsync(UploadFileRequest uploadFileRequest);

        Task<ListBlobsResponse> ListBlobsInContainerByContainerNameAsync(ListBlobsRequest listBlobsRequest);

        Task<string> DownloadBlobToLocalByContainerNameAndBlobNameAsync(DownloadBlobRequest downloadBlobRequest);

        Task<string> DeleteContainerByContainerName(DeleteContainerRequest deleteContainerRequest);

        Task<string> DeleteBlobByContainerNameAndBlobName(DeleteBlobRequest deleteBlobRequest);
    }
}
