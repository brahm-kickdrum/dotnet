using Azure.Messaging.EventHubs.Producer;
using Azure.Messaging.EventHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EventhubProducer
{
    public class EventProducer
    {
        private readonly string _connectionString;
        private readonly string _eventHubName;

        public EventProducer(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("EventHubSend:ConnectionString").Value;
            _eventHubName = configuration.GetSection("EventHubSend:Name").Value;

            if (string.IsNullOrEmpty(_connectionString) || string.IsNullOrEmpty(_eventHubName))
            {
                Console.WriteLine("Connection string or event hub name is missing or empty.");
                throw new ArgumentException("Connection string or event hub name is missing or empty.");
            }
        }

        public async Task SendEventsAsync(int n)
        {
            await using (var producerClient = new EventHubProducerClient(_connectionString, _eventHubName))
            {
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

                for (int i=1; i<=n; i++)
                {
                    eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"event {i}")));
                }

                await producerClient.SendAsync(eventBatch);
                Console.WriteLine($"A batch of {n} events has been published.");
            }
        }
    }
}
