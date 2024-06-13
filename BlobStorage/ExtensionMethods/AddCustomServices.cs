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
            services.AddScoped<IKeyVaultService, KeyVaultService>();
            services.AddScoped<IBlobStorageService, BlobStorageService>();
        }
    }
}
