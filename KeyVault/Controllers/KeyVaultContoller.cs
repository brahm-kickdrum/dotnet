using KeyVault.Exceptions;
using KeyVault.Models;
using KeyVault.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace KeyVault.Controllers
{
    [Route("api/secret")]
    [ApiController]
    public class KeyVaultContoller : ControllerBase
    {
        private readonly IKeyVaultService _keyVaultService;

        public KeyVaultContoller(IKeyVaultService keyVaultService)
        {
            _keyVaultService = keyVaultService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateSecretAsync(SecretCreateRequest secretCreateRequest)
        {
            try
            {
                string result = await _keyVaultService.CreateSecretAsync(secretCreateRequest);
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

        [HttpGet("retrieve/{secretName}")]
        public async Task<ActionResult<string>> RetrieveSecretAsync(string secretName)
        {
            try
            {
                string result = await _keyVaultService.RetrieveSecretAsync(secretName);
                return Ok(result);
            }
            catch (ConfigurationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError , ex.Message);
            }
            catch (KeyVaultOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("delete/{secretName}")]
        public async Task<ActionResult<string>> DeleteSecretAsync(string secretName)
        {
            try
            {
                string result = await _keyVaultService.DeleteSecretAsync(secretName);
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

        [HttpDelete("purge/{secretName}")]
        public async Task<ActionResult<string>> PurgeSecretAsync(string secretName)
        {
            try
            {
                string result = await _keyVaultService.PurgeSecretAsync(secretName);
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
