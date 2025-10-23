using System.Runtime.Versioning;

namespace BlazorShWebsite.Client.Js;

public static class ServiceCollectionExtensions
{
    [SupportedOSPlatform("browser")]
    public static IServiceCollection AddJsInterop(this IServiceCollection services)
    {
        services.AddTransient<LocalStorage>();
        return services;
    }
}