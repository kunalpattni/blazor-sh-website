using Microsoft.JSInterop;

namespace BlazorShWebsite.Client.Js;

public class LocalStorage(IJSRuntime? js)
{
    public async Task<int> Length()
    {
        EnsureIJSRuntimeExists();
        
        return await js!.InvokeAsync<int>("eval", "localStorage.length");
    }
    
    public async Task<string?> Key(int index)
    {
        EnsureIJSRuntimeExists();
        
        return await js!.InvokeAsync<string?>("eval", $"localStorage.key({index})");
    }
    
    public async Task<string?> GetItem(string key)
    {
        EnsureIJSRuntimeExists();
        
        return await js!.InvokeAsync<string?>("eval", $"localStorage.getItem('{key}')");
    }
    
    public async Task SetItem(string key, string value)
    {
        EnsureIJSRuntimeExists();
        
        await js!.InvokeVoidAsync("eval", $"localStorage.setItem('{key}', '{value}')");
    }
    
    public async Task RemoveItem(string key)
    {
        EnsureIJSRuntimeExists();
        
        await js!.InvokeVoidAsync("eval", $"localStorage.removeItem('{key}')");
    }
    
    public async Task Clear()
    {
        EnsureIJSRuntimeExists();
        
        await js!.InvokeVoidAsync("eval", "localStorage.clear()");
    }

    //     Storage.getItem()
    // When passed a key name, will return that key's value.
    //
    //     Storage.setItem()
    //     When passed a key name and value, will add that key to the storage, or update that key's value if it already exists.
    //
    //     Storage.removeItem()
    //     When passed a key name, will remove that key from the storage.
    //
    //     Storage.clear()
    // When invoked, will empty all keys out of the storage.

    public void EnsureIJSRuntimeExists()
    {
        if (js is null)
        {
            throw new JSException("IJSRuntime does not exist");
        }
    }
}