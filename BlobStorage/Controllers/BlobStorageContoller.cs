using BlobStorage.Exceptions;
using BlobStorage.Models.Request;
using BlobStorage.Models.Response;
using BlobStorage.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace BlobStorage.Controllers
{
    [Route("api/blob-storage")]
    [ApiController]
    public class BlobStorageContoller : ControllerBase
    {
        private readonly IBlobStorageService _blobStorageService;

        public BlobStorageContoller(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [HttpPost("create-container")]
        public async Task<ActionResult<string>> CreateContainer(CreateContainerRequest createContainerRequest)
        {
            try
            {
                string containerName = await _blobStorageService.CreateContainerByContainerNameAsync(createContainerRequest);
                return Ok(containerName);
            }
            catch (ConfigurationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (KeyVaultOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (BlobStorageOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("set-access-policy")]
        public async Task<ActionResult<string>> SetAccessPolicy(SetAccessPolicyRequest setAccessPolicyRequest)
        {
            try
            {
                string response = await _blobStorageService.SetAccessPolicyByContainerNameAsync(setAccessPolicyRequest);
                return Ok(response);
            }
            catch (ConfigurationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (KeyVaultOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (BlobStorageOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("upload-blob")]
        public async Task<ActionResult<string>> UploadFile(UploadFileRequest uploadFileRequest)
        {
            try
            {
                string response = await _blobStorageService.UploadFileToContainerAsync(uploadFileRequest);
                return Ok(response);
            }
            catch (ConfigurationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (KeyVaultOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (BlobStorageOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("list-blobs")]
        public async Task<ActionResult<ListBlobsResponse>> ListBlobs([FromQuery] ListBlobsRequest listBlobsRequest)
        {
            try
            {
                ListBlobsResponse response = await _blobStorageService.ListBlobsInContainerByContainerNameAsync(listBlobsRequest);
                return Ok(response);
            }
            catch (ConfigurationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (KeyVaultOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (BlobStorageOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("download-blobs")]
        public async Task<ActionResult<string>> DownloadBlobs(DownloadBlobRequest downloadBlobRequest)
        {
            try
            {
                string response = await _blobStorageService.DownloadBlobToLocalByContainerNameAndBlobNameAsync(downloadBlobRequest);
                return Ok(response);
            }
            catch (ConfigurationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (KeyVaultOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (BlobStorageOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("delete-container")]
        public async Task<ActionResult<string>> DeleteContainer(DeleteContainerRequest deleteContainerRequest)
        {
            try
            {
                string response = await _blobStorageService.DeleteContainerByContainerName(deleteContainerRequest);
                return Ok(response);
            }
            catch (ConfigurationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (KeyVaultOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (BlobStorageOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("delete-blob")]
        public async Task<ActionResult<string>> DeleteBlob(DeleteBlobRequest deleteBlobRequest)
        {
            try
            {
                string response = await _blobStorageService.DeleteBlobByContainerNameAndBlobName(deleteBlobRequest);
                return Ok(response);
            }
            catch (ConfigurationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (KeyVaultOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (BlobStorageOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
