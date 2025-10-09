using Microsoft.JSInterop;

namespace BlazorShWebsite.Client.Js;

public class Element(IJSRuntime? js) : JsApi(js)
{
    private readonly IJSRuntime? _js = js;

    public async Task<double> ClientHeight(string id)
    {
        EnsureIJSRuntimeExists();
        
        return await _js!.InvokeAsync<double>("eval", $"document.getElementById('{id}').clientHeight");
    }
    
    public async Task<double> ClientWidth(string id)
    {
        EnsureIJSRuntimeExists();
        
        return await _js!.InvokeAsync<double>("eval", $"document.getElementById('{id}').clientWidth");
    }
}