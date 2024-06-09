using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventhubProcessor
{
    public class EventProcessor
    {
        private readonly IConfiguration _configuration;
        private readonly string _consumerGroup;
        private readonly BlobContainerClient _storageClient;
        private readonly EventProcessorClient _processor;

        public EventProcessor(IConfiguration configuration)
        {
            _configuration = configuration;
            _consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            string eventHubConnectionString = _configuration.GetSection("EventHubListen:ConnectionString").Value;
            string eventHubName = _configuration.GetSection("EventHubListen:Name").Value;
            string blobStorageConnectionString = _configuration.GetSection("BlobStorage:ConnectionString").Value;
            string blobContainerName = _configuration.GetSection("BlobStorage:ContainerName").Value;

            _storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);

            _processor = new EventProcessorClient(_storageClient, _consumerGroup, eventHubConnectionString, eventHubName);
            _processor.ProcessEventAsync += ProcessEventHandler;
            _processor.ProcessErrorAsync += ProcessErrorHandler;
        }

        public async Task StartProcessingAsync()
        {
            await _processor.StartProcessingAsync();
        }

        public async Task StopProcessingAsync()
        {
            await _processor.StopProcessingAsync();
        }

        private async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            Console.WriteLine("\tReceived event: {0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));
            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }

        private Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            Console.WriteLine($"\tPartition '{eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
            Console.WriteLine(eventArgs.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
