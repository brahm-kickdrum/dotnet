namespace EventHub.Services.IServices
{
    public interface IEventProcessorService
    {
        Task<string> StartProcessingAsync();

        Task<string> StopProcessingAsync();
    }
}
