using Microsoft.JSInterop;

namespace BlazorShWebsite.Client.Js;

public abstract class JsApi(IJSRuntime? js)
{
    protected void EnsureIJSRuntimeExists()
    {
        if (js is null)
        {
            throw new JSException("IJSRuntime does not exist");
        }
    }
}