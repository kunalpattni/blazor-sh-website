namespace BlazorShWebsite.Client.Js;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJsInterop(this IServiceCollection services)
    {
        services.AddTransient<Element>();
        services.AddTransient<LocalStorage>();
        return services;
    }
}