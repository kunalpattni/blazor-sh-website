using Microsoft.JSInterop;

namespace BlazorShWebsite.Client.Js;

public class LocalStorage(IJSRuntime? js, ILogger<LocalStorage> logger) : JsApi(js, logger)
{
    private readonly IJSRuntime? _js = js;

    public async Task<int?> Length()
    {
        if(!TryEnsureIJsRuntimeExists())
        {
            return null;
        }
        
        return await _js!.InvokeAsync<int>("eval", "localStorage.length");
    }
    
    public async Task<string?> Key(int index)
    {
        if(!TryEnsureIJsRuntimeExists()) 
        {
            return null;
        }
        
        return await _js!.InvokeAsync<string?>("eval", $"localStorage.key({index})");
    }
    
    public async Task<string?> GetItem(string key)
    {
        if(!TryEnsureIJsRuntimeExists()) 
        {
            return null;
        }
        
        return await _js!.InvokeAsync<string?>("eval", $"localStorage.getItem('{key}')");
    }
    
    public async Task<bool> SetItem(string key, string value)
    {
        if(!TryEnsureIJsRuntimeExists()) 
        {
            return false;
        }
        
        await _js!.InvokeVoidAsync("eval", $"localStorage.setItem('{key}', '{value}')");
        return true;
    }
    
    public async Task<bool> RemoveItem(string key)
    {
        if(!TryEnsureIJsRuntimeExists()) 
        {
            return false;
        }
        
        await _js!.InvokeVoidAsync("eval", $"localStorage.removeItem('{key}')");
        return true;
    }
    
    public async Task<bool> Clear()
    {
        if(!TryEnsureIJsRuntimeExists()) 
        {
            return false;
        }
        
        await _js!.InvokeVoidAsync("eval", "localStorage.clear()");
        return true;
    }
}