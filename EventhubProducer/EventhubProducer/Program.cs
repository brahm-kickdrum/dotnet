using System;
using System.Text;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using EventhubProducer;
using Microsoft.Extensions.Configuration;

namespace EventhubProducer
{
    public class Program
    {
        static async Task Main()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var sender = new EventProducer(configuration);
            await sender.SendEventsAsync(3);
        }
    }
}
