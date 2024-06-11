using EventHub.Services.Implementations;
using EventHub.Services.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventHub.Controllers
{
    [Route("api/event-processor")]
    [ApiController]
    public class EventProcessorController : ControllerBase
    {
        private readonly IEventProcessorService _eventProcessorService;

        public EventProcessorController(IEventProcessorService eventProcessorService)
        {
            _eventProcessorService = eventProcessorService;
        }

        [HttpPost("start")]
        public async Task<ActionResult<string>> StartProcessing()
        {
            string result = await _eventProcessorService.StartProcessingAsync();
            return Ok(result);
        }

        [HttpPost("stop")]
        public async Task<ActionResult<string>> StopProcessing()
        {
            string result = await _eventProcessorService.StopProcessingAsync();
            return Ok(result);
        }

    }
}
