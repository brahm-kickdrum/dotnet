using KeyVault.Constants;
using KeyVault.Exceptions;
using KeyVault.Models.Requests;
using KeyVault.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace KeyVault.Controllers
{
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class KeyVaultContoller : ControllerBase
    {
        private readonly IKeyVaultService _keyVaultService;

        public KeyVaultContoller(IKeyVaultService keyVaultService)
        {
            _keyVaultService = keyVaultService;
        }

        [HttpPost(RouteConstants.CreateSecret)]
        public async Task<ActionResult<string>> CreateSecretAsync(SecretCreateRequest secretCreateRequest)
        {
            try
            {
                string result = await _keyVaultService.CreateSecretBySecretNameAndValueAsync(secretCreateRequest);
                return Ok(result);
            }
            catch (ConfigurationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (KeyVaultOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet(RouteConstants.RetrieveSecret)]
        public async Task<ActionResult<string>> RetrieveSecretAsync([FromQuery] SecretRetrieveRequest secretRetrieveRequest)
        {
            try
            {
                string result = await _keyVaultService.RetrieveSecretBySecretNameAsync(secretRetrieveRequest);
                return Ok(result);
            }
            catch (ConfigurationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (KeyVaultOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete(RouteConstants.DeleteSecret)]
        public async Task<ActionResult<string>> DeleteSecretAsync([FromQuery] SecretDeleteRequest secretDeleteRequest)
        {
            try
            {
                string result = await _keyVaultService.DeleteSecretBySecretNameAsync(secretDeleteRequest);
                return Ok(result);
            }
            catch (ConfigurationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (KeyVaultOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete(RouteConstants.PurgeSecret)]
        public async Task<ActionResult<string>> PurgeSecretAsync([FromQuery] SecretPurgeRequest secretPurgeRequest)
        {
            try
            {
                string result = await _keyVaultService.PurgeSecretBySecretNameAsync(secretPurgeRequest);
                return Ok(result);
            }
            catch (ConfigurationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (KeyVaultOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
