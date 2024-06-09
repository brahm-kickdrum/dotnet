using System;
using System.Text;
using Azure.Storage.Blobs;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Microsoft.Extensions.Configuration;
using EventhubProcessor;

namespace EventhubProcessor
{
    class Program
    {
        static async Task Main()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            EventProcessor eventProcessor = new EventProcessor(configuration);

            await eventProcessor.StartProcessingAsync();

            await Task.Delay(TimeSpan.FromSeconds(10));

            await eventProcessor.StopProcessingAsync();
        }
    }
}