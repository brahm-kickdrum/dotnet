using KeyVault.Services.IService;
using KeyVault.Services;
using KeyVault.Middlewares;

namespace KeyVault.ExtensionMethods
{
    public static class AddCustomServices
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<GlobalExceptionHandlingMiddleware>();
            services.AddScoped<IKeyVaultService, KeyVaultService>();
        }
    }   
}
    