using Microsoft.JSInterop;

namespace BlazorShWebsite.Client.Js;

public class Element(IJSRuntime? js, ILogger<Element> logger) : JsApi(js, logger)
{
    private readonly IJSRuntime? _js = js;

    public async Task<double?> ClientHeight(string id)
    {
        if (!TryEnsureIJsRuntimeExists())
        {
            return null;
        }
        
        return await _js!.InvokeAsync<double>("eval", $"document.getElementById('{id}').clientHeight");
    }
    
    public async Task<double?> ClientWidth(string id)
    {
        if (!TryEnsureIJsRuntimeExists())
        {
            return null;
        }
        
        return await _js!.InvokeAsync<double>("eval", $"document.getElementById('{id}').clientWidth");
    }
}