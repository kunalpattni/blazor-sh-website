using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorShWebsite.Client.EveryPage;

public class PageComponent : ComponentBase
{
    [Inject] protected IJSRuntime Js { get; set; }
    [Inject] protected NavigationManager NavigationManager { get; set; }
    protected double Tti { get; private set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Tti = await Js.InvokeAsync<double>("eval", "window.performance.now() - window.tti");
            Console.WriteLine($"TTI (approx): {Tti} ms");
            StateHasChanged();
        }
        NavigationManager.RegisterLocationChangingHandler(async (_) => await Js.InvokeVoidAsync("eval", "window.tti = window.performance.now()"));
        await base.OnAfterRenderAsync(firstRender);

    }
}