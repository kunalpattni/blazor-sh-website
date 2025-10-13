using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorShWebsite.Client.EveryPage;

public class PageComponent : ComponentBase, IAsyncDisposable
{
    [Inject] protected IJSRuntime Js { get; set; }
    protected double Tti { get; private set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Tti = await Js!.InvokeAsync<double>("eval", "window.performance.now() - window.tti");
            Console.WriteLine($"TTI (approx): {Tti} ms");
            StateHasChanged();
        }
        await base.OnAfterRenderAsync(firstRender);

    }
    
    public virtual async ValueTask DisposeAsync()
    {
       await Js!.InvokeVoidAsync("eval", "window.tti = window.performance.now()");
        GC.SuppressFinalize(this);
    }
}