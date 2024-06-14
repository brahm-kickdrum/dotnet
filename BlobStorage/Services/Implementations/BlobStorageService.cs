using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobStorage.Configurations;
using BlobStorage.Constants;
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
        private readonly BlobStorageConfigurations _blobStorageConfigurations;

        public BlobStorageService(BlobStorageConfigurations blobStorageConfigurations)
        {
            _blobStorageConfigurations = blobStorageConfigurations;
            string connectionString = _blobStorageConfigurations.BlobStorageConnectionString;
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> CreateContainerByContainerNameAsync(CreateContainerRequest createContainerRequest)
        {
            try
            {
                string containerName = createContainerRequest.ContainerName;
                BlobContainerClient containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);

                return string.Format(ResponseMessages.ContainerCreatedSuccessfully ,createContainerRequest.ContainerName);
            }
            catch (Exception)
            {
                throw new BlobStorageOperationException(string.Format(ErrorMessages.ContainerCreationError, createContainerRequest.ContainerName));
            }
        }

        public async Task<string> SetAccessPolicyByContainerNameAsync(SetAccessPolicyRequest setAccessPolicyRequest)
        {
            try
            {
                BlobContainerClient? containerClient = GetBlobContainerClientByContainerName(setAccessPolicyRequest.ContainerName);
                PublicAccessType accessType = BlobAccessLevelConverter.ConvertToPublicAccessType(setAccessPolicyRequest.AccessLevel);

                await containerClient.SetAccessPolicyAsync(accessType);

                return string.Format(ResponseMessages.AccessPolicySetSuccessfully, setAccessPolicyRequest.ContainerName, accessType);
            }
            catch (Exception)
            {
                throw new BlobStorageOperationException(string.Format(ErrorMessages.AccessPolicyError, setAccessPolicyRequest.ContainerName));
            }
        }

        public async Task<string> UploadFileToContainerAsync(UploadFileRequest uploadFileRequest)
        {
            try
            {
                if (!File.Exists(uploadFileRequest.LocalFilePath))
                {
                    throw new BlobStorageOperationException(string.Format(ErrorMessages.FileNotFoundError, uploadFileRequest.LocalFilePath));
                }

                BlobContainerClient containerClient = GetBlobContainerClientByContainerName(uploadFileRequest.ContainerName);
                BlobClient blobClient = GetBlobClientByContainerClientAndFileName(containerClient, uploadFileRequest.FileName);

                await blobClient.UploadAsync(uploadFileRequest.LocalFilePath, true);

                string uri = blobClient.Uri.ToString();

                return uri;
            }
            catch (Exception ex)
            {
                throw new BlobStorageOperationException(ex.Message);
                //throw new BlobStorageOperationException(string.Format(ErrorMessages.UploadFileError, uploadFileRequest.ContainerName));
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
                throw new BlobStorageOperationException(string.Format(ErrorMessages.ListBlobsError, listBlobsRequest.ContainerName));
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
                    throw new BlobStorageOperationException(string.Format(ErrorMessages.InvalidFilePathError, downloadBlobRequest.LocalFilePath));
                }
                else if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                await blobClient.DownloadToAsync(downloadBlobRequest.LocalFilePath);

                return downloadBlobRequest.LocalFilePath;
            }
            catch (Exception)
            {
                throw new BlobStorageOperationException(ErrorMessages.DownloadBlobError);
            }
        }

        public async Task<string> DeleteContainerByContainerName(DeleteContainerRequest deleteContainerRequest)
        {
            try
            {
                BlobContainerClient containerClient = GetBlobContainerClientByContainerName(deleteContainerRequest.ContainerName);

                await containerClient.DeleteIfExistsAsync();

                return string.Format(ResponseMessages.ContainerDeletedSuccessfully, deleteContainerRequest.ContainerName);
            }
            catch (Exception)
            {
                throw new BlobStorageOperationException(string.Format(ErrorMessages.ContainerDeletionError, deleteContainerRequest.ContainerName));
            }
        }

        public async Task<string> DeleteBlobByContainerNameAndBlobName(DeleteBlobRequest deleteBlobRequest)
        {
            try
            {
                BlobContainerClient containerClient = GetBlobContainerClientByContainerName(deleteBlobRequest.ContainerName);
                BlobClient blobClient = GetBlobClientByContainerClientAndFileName(containerClient, deleteBlobRequest.BlobName);

                await blobClient.DeleteIfExistsAsync();

                return string.Format(ResponseMessages.BlobDeletedSuccessfully, deleteBlobRequest.BlobName);
            }
            catch (Exception)
            {
                throw new BlobStorageOperationException(string.Format(ErrorMessages.BlobDeletionError, deleteBlobRequest.BlobName));
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
                throw new BlobStorageOperationException(string.Format(ErrorMessages.ContainerClientError, containerName));
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
                throw new BlobStorageOperationException(string.Format(ErrorMessages.BlobClientError, fileName, containerClient.Name));
            }
        }

    }
}
