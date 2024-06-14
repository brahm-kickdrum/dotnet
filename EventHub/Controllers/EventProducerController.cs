using EventHub.Exceptions;
using EventHub.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    [Route("api/event-producer")]
    [ApiController]
    public class EventProducerController : ControllerBase
    {
        private readonly IEventProducerService _eventProducerService;

        public EventProducerController(IEventProducerService eventProducerService)
        {
            _eventProducerService = eventProducerService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(int n)
        {
            try
            {
                string result = await _eventProducerService.SendEventsAsync(n);
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
