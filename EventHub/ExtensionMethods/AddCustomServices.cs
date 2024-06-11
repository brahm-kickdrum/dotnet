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
            services.AddScoped<IKeyVaultService, KeyVaultService>();
            services.AddScoped<IEventProcessorService, EventProcessorService>();
            services.AddScoped<IEventProducerService, EventProducerService>();
        }
    }
}
