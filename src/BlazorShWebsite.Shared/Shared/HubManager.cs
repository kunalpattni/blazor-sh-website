using Microsoft.AspNetCore.Components;
using SharedNotes = BlazorShWebsite.Shared.Notes;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace BlazorShWebsite.Shared.Shared;

public class HubManager : IAsyncDisposable
{
    private readonly ILogger<HubManager> _logger;
    public HubConnection HubConnection { get;}
    
    public HubManager(ILogger<HubManager> logger, NavigationManager navigationManager)
    {
        _logger = logger;
        HubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri(SharedNotes.Constants.PartialHubPath.ToHubUrl()))
            .Build();
        
        HubConnection.On("Server", (SharedNotes.Model model) =>
        {
            _logger.LogInformation("received {model}", model.ToString());
        });
        
        HubConnection.Closed += async _ =>
        {
            await Task.Delay(new Random().Next(0,5) * 1000);
            await HubConnection.StartAsync();
        };
    }

    public async Task Start()
    {
        await HubConnection.StartAsync();
    }

    public async Task Send(SharedNotes.Model model)
    {
        await HubConnection.StartAsync();
        await HubConnection.SendAsync("Client", model);
        _logger.LogInformation("sent {model}", model.ToString());
    }


    public async ValueTask DisposeAsync()
    {
        await HubConnection.DisposeAsync();
    }
}