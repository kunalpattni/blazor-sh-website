using System.Runtime.Versioning;

namespace BlazorShWebsite.Client.Js;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJsInterop(this IServiceCollection services)
    {
        services.AddTransient<LocalStorage>();
        return services;
    }
}