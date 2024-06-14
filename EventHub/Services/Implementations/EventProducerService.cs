using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using EventHub.Configurations;
using EventHub.Constants;
using EventHub.Exceptions;
using EventHub.Services.IServices;
using Microsoft.AspNetCore.Components.Web;
using System.Runtime.InteropServices;
using System.Text;

namespace EventHub.Services.Implementations
{
    public class EventProducerService : IEventProducerService
    {
        private readonly EventHubConfigurations _eventHubConfigurations;

        public EventProducerService(EventHubConfigurations eventHubConfigurations)
        {
            _eventHubConfigurations = eventHubConfigurations;
        }

        public async Task<string> SendEventsAsync(int n)
        {
            string connectionString = _eventHubConfigurations.EventHubSendConnectionString;
            string eventHubName = _eventHubConfigurations.EventHubSendName;

            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(eventHubName))
            {
                throw new ConfigurationException(ErrorMessages.ConfigurationError);
            }

            await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
            {
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

                for (int i = 1; i <= n; i++)
                {
                    eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(string.Format(AppConstants.Event, i))));
                }

                await producerClient.SendAsync(eventBatch);
                return string.Format(ResponseMessages.EventsSentFormat, n);
            }
        }
    }
}
