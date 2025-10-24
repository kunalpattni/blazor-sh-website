using System.Runtime.Versioning;
using BlazorShWebsite.Client.Js;

namespace BlazorShWebsite.Client.Services.Mileage;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMileageTracker(this IServiceCollection services)
    {
        services.AddTransient<MileageStateManager>();
        return services;
    }
}