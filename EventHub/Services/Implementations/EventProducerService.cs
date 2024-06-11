using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using EventHub.Services.IServices;
using Microsoft.AspNetCore.Components.Web;
using System.Runtime.InteropServices;
using System.Text;

namespace EventHub.Services.Implementations
{
    public class EventProducerService : IEventProducerService
    {
        private readonly IKeyVaultService _keyVaultService;

        public EventProducerService(IKeyVaultService keyVaultService)
        {
            _keyVaultService = keyVaultService;
        }

        public async Task<string> SendEventsAsync(int n)
        {
            string connectionString = await _keyVaultService.RetrieveSecretAsync("EventHubSendConnectionString");
            string eventHubName = await _keyVaultService.RetrieveSecretAsync("EventHubSendName");

            await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
            {
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

                for (int i = 1; i <= n; i++)
                {
                    eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"event {i}")));
                }

                await producerClient.SendAsync(eventBatch);
                return $"A batch of {n} events has been published.";
            }
        }
    }
}
