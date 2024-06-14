using EventHub.Constants;

namespace EventHub.Configurations
{
    public class EventHubConfigurations
    {
        public string EventHubSendConnectionString { get; set; }
        public string EventHubSendName { get; set; }
        public string EventHubListenConnectionString { get; set; }
        public string EventHubListenName { get; set; }

        public EventHubConfigurations(string eventHubSendConnectionString, string eventHubSendName, string eventHubListenConnectionString, string eventHubListenName)
        {
            EventHubSendConnectionString = eventHubSendConnectionString;
            EventHubSendName = eventHubSendName;
            EventHubListenConnectionString = eventHubListenConnectionString;
            EventHubListenName = eventHubListenName;
        }
    }
}
