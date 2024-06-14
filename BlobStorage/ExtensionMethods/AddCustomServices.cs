using BlobStorage.Configurations;
using BlobStorage.Constants;
using BlobStorage.Middleware;
using BlobStorage.Services.Implementations;
using BlobStorage.Services.IServices;

namespace BlobStorage.ExtensionMethods
{
    public static class AddCustomServices
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<GlobalExceptionHandlingMiddleware>();
            services.AddScoped<IBlobStorageService, BlobStorageService>();

            services.AddSingleton<IKeyVaultService, KeyVaultService>();
            services.AddSingleton(provider =>
            {
                IKeyVaultService keyVaultService = provider.GetRequiredService<IKeyVaultService>();
                string connectionString = keyVaultService.RetrieveSecretAsync(AppConstants.BlobStorageConnectionString).GetAwaiter().GetResult();

                return new BlobStorageConfigurations(connectionString);
            });
        }
    }
}
