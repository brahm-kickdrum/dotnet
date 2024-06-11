using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs;
using Azure.Storage.Blobs;
using EventHub.Services.IServices;
using System.Text;
using Serilog;
using EventHub.Exceptions;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace EventHub.Services.Implementations
{
    public class EventProcessorService : IEventProcessorService
    {
        private readonly IKeyVaultService _keyVaultService;
        private readonly EventProcessorClient _processor;
        private readonly ILogger<EventProcessorService> _logger;

        public EventProcessorService(IKeyVaultService keyVaultService, ILogger<EventProcessorService> logger)
        {
            _keyVaultService = keyVaultService;
            _processor = InitializeProcessorAsync().GetAwaiter().GetResult();
            _logger = logger;
        }

        private async Task<EventProcessorClient> InitializeProcessorAsync()
        {
            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            string eventHubConnectionString = await _keyVaultService.RetrieveSecretAsync("EventHubListenConnectionString");
            string eventHubName = await _keyVaultService.RetrieveSecretAsync("EventHubListenName");
            string blobStorageConnectionString = await _keyVaultService.RetrieveSecretAsync("BlobStorageConnectionString");
            string blobContainerName = await _keyVaultService.RetrieveSecretAsync("BlobStorageContainerName");

            if (string.IsNullOrEmpty(eventHubConnectionString) || string.IsNullOrEmpty(eventHubName) || string.IsNullOrEmpty(blobStorageConnectionString) || string.IsNullOrEmpty(blobContainerName))
            {
                throw new ConfigurationException("Oops! It looks like something went wrong while trying to connect.");
            }

            BlobContainerClient storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);

            EventProcessorClient processor = new EventProcessorClient(storageClient, consumerGroup, eventHubConnectionString, eventHubName);
            processor.ProcessEventAsync += ProcessEventHandler;
            processor.ProcessErrorAsync += ProcessErrorHandler;

            return processor;
        }

        public async Task<string> StartProcessingAsync()
        {
            await _processor.StartProcessingAsync();
            return "Processing started successfully";
        }

        public async Task<string> StopProcessingAsync()
        {
            await _processor.StopProcessingAsync();
            return "Processing stopped successfully";
        }

        private async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            string eventData = Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray());
            _logger.LogInformation("Received event: {EventData}", eventData);
            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }

        private Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            _logger.LogError(eventArgs.Exception, $"Error processing event from partition {eventArgs.PartitionId}", eventArgs.PartitionId);
            return Task.CompletedTask;
        }
    }
}
