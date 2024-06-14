using BlobStorage.Constants;
using BlobStorage.Exceptions;
using BlobStorage.Models.Request;
using BlobStorage.Models.Response;
using BlobStorage.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace BlobStorage.Controllers
{
    [Route(RouteConstants.BlobStorageApi)]
    [ApiController]
    public class BlobStorageContoller : ControllerBase
    {
        private readonly IBlobStorageService _blobStorageService;

        public BlobStorageContoller(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [HttpPost(RouteConstants.CreateContainer)]
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

        [HttpPost(RouteConstants.SetAccessPolicy)]
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

        [HttpPost(RouteConstants.UploadBlob)]
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

        [HttpGet(RouteConstants.ListBlobs)]
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

        [HttpPost(RouteConstants.DownloadBlob)]
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

        [HttpDelete(RouteConstants.DeleteContainer)]
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

        [HttpDelete(RouteConstants.DeleteBlob)]
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
