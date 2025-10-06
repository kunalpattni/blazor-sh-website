using Microsoft.AspNetCore.Components;
using SharedNotes = BlazorShWebsite.Shared.Notes;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace BlazorShWebsite.Shared.Shared;

public class HubManager : IAsyncDisposable
{
    private readonly ILogger<HubManager> _logger;
    private readonly NavigationManager _navigationManager;
    private bool _isDisposed;
    private HubConnection? _hubConnection;

    public HubConnectionState State
    {
        get
        {
            if (_hubConnection is null || _isDisposed)
            {
                return HubConnectionState.Disconnected;
            }
            return _hubConnection.State;
        }
    }

    public HubManager(ILogger<HubManager> logger, NavigationManager navigationManager)
    {
        _logger = logger;
        _navigationManager = navigationManager;
        Create();
    }

    private void Create()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(_navigationManager.ToAbsoluteUri(SharedNotes.Constants.PartialHubPath.ToHubUrl()))
            .WithAutomaticReconnect()
            .Build();
        _isDisposed = false;
        
        _hubConnection.On("Server", (SharedNotes.Model model) =>
        {
            _logger.LogInformation("received {model}", model.ToString());
        });
        
        _hubConnection.Closed += async _ =>
        {
            await Task.Delay(new Random().Next(0,5) * 1000);
            await _hubConnection.StartAsync();
        };
        
        // _hubConnection.Reconnected
        // _hubConnection.Reconnecting
    }

    private async Task EnsureConnected()
    {
        if (_hubConnection is null || _isDisposed)
        {
            Create();
        }

        if (_hubConnection!.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }
    }

    public async Task Send(SharedNotes.Model model)
    {
        await EnsureConnected();
        await _hubConnection.SendAsync("Client", model);
        _logger.LogInformation("sent {model}", model.ToString());
    }


    public async ValueTask DisposeAsync()
    {
        if (_hubConnection != null)
        {
            try
            {
                await _hubConnection.DisposeAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "hub connection could not be disposed");
            }
        }
        _hubConnection = null;
        _isDisposed = true;
        await Task.CompletedTask;
    }
}