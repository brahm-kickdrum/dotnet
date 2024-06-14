using EventHub.Configurations;
using EventHub.Constants;
using EventHub.Middlewares;
using EventHub.Services.Implementations;
using EventHub.Services.IServices;

namespace EventHub.ExtensionMethods
{
    public static class AddCustomServices
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<GlobalExceptionHandlingMiddleware>();

            services.AddScoped<IEventProducerService, EventProducerService>();

            services.AddScoped<IEventProcessorService, EventProcessorService>();

            services.AddSingleton<IKeyVaultService, KeyVaultService>();

            services.AddSingleton(provider =>
            {
                IKeyVaultService keyVaultService = provider.GetRequiredService<IKeyVaultService>();
                string blobStorageConnectionString = keyVaultService.RetrieveSecretAsync(AppConstants.BlobStorageConnectionString).GetAwaiter().GetResult();
                string blobStorageContainerName = keyVaultService.RetrieveSecretAsync(AppConstants.BlobStorageContainerName).GetAwaiter().GetResult();

                return new BlobStorageConfigurations(blobStorageConnectionString, blobStorageContainerName);
            });

            services.AddSingleton(provider =>
            {
                IKeyVaultService keyVaultService = provider.GetRequiredService<IKeyVaultService>();
                string eventHubSendConnectionString = keyVaultService.RetrieveSecretAsync(AppConstants.EventHubSendConnectionString).GetAwaiter().GetResult();
                string eventHubSendName = keyVaultService.RetrieveSecretAsync(AppConstants.EventHubSendName).GetAwaiter().GetResult();
                string eventHubListenConnectionString = keyVaultService.RetrieveSecretAsync(AppConstants.EventHubListenConnectionString).GetAwaiter().GetResult();
                string eventHubListenName = keyVaultService.RetrieveSecretAsync(AppConstants.EventHubListenName).GetAwaiter().GetResult();

                return new EventHubConfigurations(eventHubSendConnectionString, eventHubSendName, eventHubListenConnectionString, eventHubListenName);
            });
        }
    }
}
