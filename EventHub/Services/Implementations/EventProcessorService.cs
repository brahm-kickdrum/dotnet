using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs;
using Azure.Storage.Blobs;
using EventHub.Services.IServices;
using System.Text;
using Serilog;
using EventHub.Exceptions;

namespace EventHub.Services.Implementations
{
    public class EventProcessorService : IEventProcessorService
    {
        private readonly IKeyVaultService _keyVaultService;
        private readonly EventProcessorClient _processor;

        public EventProcessorService(IKeyVaultService keyVaultService)
        {
            _keyVaultService = keyVaultService;
            _processor = InitializeProcessorAsync().GetAwaiter().GetResult();
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
            Console.WriteLine("\tReceived event: {0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));
            Log.Information(Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));
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
