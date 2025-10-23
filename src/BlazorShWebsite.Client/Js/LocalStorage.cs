using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace BlazorShWebsite.Client.Js;

[SupportedOSPlatform("browser")]
public partial class LocalStorage
{
    [JSImport("globalThis.localStorage.getItem")]
    public static partial string GetItem(string key);
}