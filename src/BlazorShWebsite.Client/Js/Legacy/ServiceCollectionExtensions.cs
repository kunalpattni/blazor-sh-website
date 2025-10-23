namespace BlazorShWebsite.Client.Js.Legacy;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJsInteropLegacy(this IServiceCollection services)
    {
        services.AddTransient<Element>();
        services.AddTransient<LocalStorage>();
        return services;
    }
}