using Microsoft.JSInterop;

namespace BlazorShWebsite.Client.Js.Legacy;

public abstract class JsApi(IJSRuntime? js, ILogger<JsApi> logger)
{
    protected void EnsureIJsRuntimeExists()
    {
        if (js is null)
        {
            throw new JSException("IJSRuntime does not exist");
        }
    }

    protected bool TryEnsureIJsRuntimeExists()
    {
        try
        {
            EnsureIJsRuntimeExists();
        }
        catch (JSException ex)
        {
            logger.LogWarning(ex, "IJsRuntime does not exist");
            return false;
        }

        return true;
    }
}