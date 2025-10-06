using Microsoft.JSInterop;

namespace BlazorShWebsite.Client.Js;

public class Element(IJSRuntime? js)
{
    public async Task<double> ClientHeight(string id)
    {
        EnsureIJSRuntimeExists();
        
        return await js!.InvokeAsync<double>("eval", $"document.getElementById('{id}').clientHeight");
    }
    
    public async Task<double> ClientWidth(string id)
    {
        EnsureIJSRuntimeExists();
        
        return await js!.InvokeAsync<double>("eval", $"document.getElementById('{id}').clientWidth");
    }

    public void EnsureIJSRuntimeExists()
    {
        if (js is null)
        {
            throw new JSException("IJSRuntime does not exist");
        }
    }
}