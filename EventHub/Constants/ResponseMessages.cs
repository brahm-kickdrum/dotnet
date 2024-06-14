namespace EventHub.Constants
{
    public static class ResponseMessages
    {
        public const string ProcessingStarted = "Processing started successfully";
        public const string ProcessingStopped = "Processing stopped successfully";
        public const string EventsSentFormat = "A batch of {0} events has been published.";
        public const string RecievedEvent = "Received event: {0}";
    }
}
