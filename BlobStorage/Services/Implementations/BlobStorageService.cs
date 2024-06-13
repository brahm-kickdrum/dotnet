using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobStorage.Enums;
using BlobStorage.Exceptions;
using BlobStorage.Models.Request;
using BlobStorage.Models.Response;
using BlobStorage.Services.IServices;
using BlobStorage.Utils;

namespace BlobStorage.Services.Implementations
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IKeyVaultService _keyVaultService;

        public BlobStorageService(IKeyVaultService keyVaultService)
        {
            _keyVaultService = keyVaultService;
            string connectionString = GetStorageConnectionStringBySecretName("BlobStorageConnectionString").GetAwaiter().GetResult();
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        private async Task<string> GetStorageConnectionStringBySecretName(string secretName)
        {
            try
            {
                return await _keyVaultService.RetrieveSecretAsync(secretName);
            }
            catch (Exception)
            {
                throw new ConfigurationException("Oops! It looks like something went wrong while trying to connect.");
            }
        }

        public async Task<string> CreateContainerByContainerNameAsync(CreateContainerRequest createContainerRequest)
        {
            try
            {
                string containerName = createContainerRequest.ContainerName;
                BlobContainerClient containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);
                
                return $"Container {createContainerRequest.ContainerName} created successfully.";
            }
            catch (Exception)
            {
                throw new BlobStorageOperationException($"An error occurred while creating container '{createContainerRequest.ContainerName}'");
            }
        }

        public async Task<string> SetAccessPolicyByContainerNameAsync(SetAccessPolicyRequest setAccessPolicyRequest)
        {
            try
            {
                BlobContainerClient? containerClient = GetBlobContainerClientByContainerName(setAccessPolicyRequest.ContainerName);

                PublicAccessType accessType = BlobAccessLevelConverter.ConvertToPublicAccessType(setAccessPolicyRequest.AccessLevel);

                await containerClient.SetAccessPolicyAsync(accessType);

                return $"Access policy for container {setAccessPolicyRequest.ContainerName} is set to {accessType}";
            }
            catch (Exception)
            {
                throw new BlobStorageOperationException($"An error occurred while setting access policy for container '{setAccessPolicyRequest.ContainerName}'");
            }
        }

        public async Task<string> UploadFileToContainerAsync(UploadFileRequest uploadFileRequest)
        {
            try
            {
                if(!File.Exists(uploadFileRequest.LocalFilePath))
                {
                    throw new BlobStorageOperationException($"File not found at {uploadFileRequest.LocalFilePath}.");
                }

                BlobContainerClient containerClient = GetBlobContainerClientByContainerName(uploadFileRequest.ContainerName);
                BlobClient blobClient = GetBlobClientByContainerClientAndFileName(containerClient, uploadFileRequest.FileName);

                await blobClient.UploadAsync(uploadFileRequest.LocalFilePath, true);

                string uri = blobClient.Uri.ToString();

                return uri;
            }
            catch (Exception)
            {
                throw new BlobStorageOperationException($"An error occurred while uploading file to container '{uploadFileRequest.ContainerName}'");
            }
        }

        public async Task<ListBlobsResponse> ListBlobsInContainerByContainerNameAsync(ListBlobsRequest listBlobsRequest)
        {
            try
            {
                BlobContainerClient containerClient = GetBlobContainerClientByContainerName(listBlobsRequest.ContainerName);
                List<string> blobNames = new List<string>();

                await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
                {
                    blobNames.Add(blobItem.Name);
                }

                return new ListBlobsResponse(blobNames);
            }
            catch (Exception)
            {
                throw new BlobStorageOperationException($"Failed to list blobs in container {listBlobsRequest.ContainerName}.");
            }
        }

        public async Task<string> DownloadBlobToLocalByContainerNameAndBlobNameAsync(DownloadBlobRequest downloadBlobRequest)
        {
            try
            {
                BlobContainerClient containerClient = GetBlobContainerClientByContainerName(downloadBlobRequest.ContainerName);
                BlobClient blobClient = GetBlobClientByContainerClientAndFileName(containerClient, downloadBlobRequest.BlobName);

                string? directory = Path.GetDirectoryName(downloadBlobRequest.LocalFilePath);

                if (directory == null)
                {
                    throw new BlobStorageOperationException($"Invalid local file path: {downloadBlobRequest.LocalFilePath}.");
                }
                else if(!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                await blobClient.DownloadToAsync(downloadBlobRequest.LocalFilePath);

                return downloadBlobRequest.LocalFilePath;
            }
            catch (Exception)
            {
                throw new BlobStorageOperationException("Failed to download blob.");
            }
        }

        public async Task<string> DeleteContainerByContainerName(DeleteContainerRequest deleteContainerRequest)
        {
            try
            {
                BlobContainerClient containerClient = GetBlobContainerClientByContainerName(deleteContainerRequest.ContainerName);

                await containerClient.DeleteIfExistsAsync();

                return $"Container {deleteContainerRequest.ContainerName} deleted successfully.";
            }
            catch (Exception)
            {
                throw new BlobStorageOperationException($"An error occurred while deleting container {deleteContainerRequest.ContainerName}");
            }
        }

        public async Task<string> DeleteBlobByContainerNameAndBlobName(DeleteBlobRequest deleteBlobRequest)
        {
            try
            {
                BlobContainerClient containerClient = GetBlobContainerClientByContainerName(deleteBlobRequest.ContainerName);
                BlobClient blobClient = GetBlobClientByContainerClientAndFileName(containerClient, deleteBlobRequest.BlobName);

                await blobClient.DeleteIfExistsAsync();

                return $"Blob {deleteBlobRequest.BlobName} deleted successfully.";
            }
            catch (Exception)
            {
                throw new BlobStorageOperationException($"An error occurred while deleting blob {deleteBlobRequest.BlobName}");
            }
        }

        private BlobContainerClient GetBlobContainerClientByContainerName(string containerName)
        {
            try
            {
                return _blobServiceClient.GetBlobContainerClient(containerName);
            }
            catch (Exception)    
            {
                throw new BlobStorageOperationException($"An error occurred while getting BlobContainerClient for container '{containerName}'");
            }
        }

        private BlobClient GetBlobClientByContainerClientAndFileName(BlobContainerClient containerClient, string fileName)
        {
            try
            {
                return containerClient.GetBlobClient(fileName);
            }
            catch (Exception)
            {
                throw new BlobStorageOperationException($"An error occurred while getting BlobClient for file '{fileName}' in container '{containerClient.Name}'");
            }
        }

    }
}
