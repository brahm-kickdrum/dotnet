namespace EventHub.Services.IServices
{
    public interface IEventProducerService
    {
        Task<string> SendEventsAsync(int n);
    }
}
