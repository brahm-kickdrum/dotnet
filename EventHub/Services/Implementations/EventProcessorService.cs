using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs;
using Azure.Storage.Blobs;
using EventHub.Services.IServices;
using System.Text;
using EventHub.Exceptions;
using EventHub.Constants;
using EventHub.Configurations;

namespace EventHub.Services.Implementations
{
    public class EventProcessorService : IEventProcessorService
    {
        private readonly EventProcessorClient _processor;
        private readonly ILogger<EventProcessorService> _logger;
        private readonly EventHubConfigurations _eventHubConfigurations;
        private readonly BlobStorageConfigurations _blobStorageConfigurations;

        public EventProcessorService(EventHubConfigurations eventHubConfigurations, BlobStorageConfigurations blobStorageConfigurations, ILogger<EventProcessorService> logger)
        {
            _eventHubConfigurations = eventHubConfigurations;
            _blobStorageConfigurations = blobStorageConfigurations;
            _processor = InitializeProcessorAsync();
            _logger = logger;
        }

        private EventProcessorClient InitializeProcessorAsync()
        {
            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            string eventHubConnectionString = _eventHubConfigurations.EventHubListenConnectionString;
            string eventHubName = _eventHubConfigurations.EventHubListenName;
            string blobStorageConnectionString = _blobStorageConfigurations.BlobStorageConnectionString;
            string blobContainerName = _blobStorageConfigurations.BlobStorageContainerName;

            if (string.IsNullOrEmpty(eventHubConnectionString) || string.IsNullOrEmpty(eventHubName) || string.IsNullOrEmpty(blobStorageConnectionString) || string.IsNullOrEmpty(blobContainerName))
            {
                throw new ConfigurationException(ErrorMessages.ConfigurationError);
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
            return ResponseMessages.ProcessingStarted;
        }

        public async Task<string> StopProcessingAsync()
        {
            await _processor.StopProcessingAsync();
            return ResponseMessages.ProcessingStopped;
        }

        private async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            string eventData = Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray());
            _logger.LogInformation(string.Format(ResponseMessages.RecievedEvent, eventData));
            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }

        private Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            _logger.LogError(eventArgs.Exception, string.Format(ErrorMessages.ErrorProcessingEvent), eventArgs.PartitionId);
            return Task.CompletedTask;
        }
    }
}
