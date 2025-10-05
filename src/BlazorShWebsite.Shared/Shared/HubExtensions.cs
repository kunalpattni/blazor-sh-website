namespace BlazorShWebsite.Shared.Shared;

public static class HubExtensions
{
    public static string ToHubUrl(this string partialPath)
    {
        return "/hubs/" + partialPath;
    }
}