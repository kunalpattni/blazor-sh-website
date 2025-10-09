using BlazorShWebsite.Shared.Notes;

namespace BlazorShWebsite.Server.Notes;

public class Hub : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task Client(Model model)
    {
        Console.WriteLine($"""
                           Context.ConnectionId: 
                               An anonymous user id
                               '{Context.ConnectionId}'
                               
                           Context.ConnectionId:
                               Basically server-side session store
                               {string.Join(", ", Context.Items.Select(pair => $"{pair.Key} {pair.Value}"))}
                               
                           model.Name: 
                               '{model.Input}'
                           """);
    }
}