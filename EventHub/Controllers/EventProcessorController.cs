using EventHub.Constants;
using EventHub.Exceptions;
using EventHub.Services.Implementations;
using EventHub.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    [Route(RouteConstants.EventProcessorBase)]
    [ApiController]
    public class EventProcessorController : ControllerBase
    {
        private readonly IEventProcessorService _eventProcessorService;

        public EventProcessorController(IEventProcessorService eventProcessorService)
        {
            _eventProcessorService = eventProcessorService;
        }

        [HttpPost(RouteConstants.Start)]
        public async Task<ActionResult<string>> StartProcessing()
        {
            try
            {
                string result = await _eventProcessorService.StartProcessingAsync();
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

        [HttpPost(RouteConstants.Stop)]
        public async Task<ActionResult<string>> StopProcessing()
        {
            try
            {
                string result = await _eventProcessorService.StopProcessingAsync();
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
