using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace BlazorLeafletInterop;

[SupportedOSPlatform("browser")]
public static class MapBundleImport
{
    public static async Task ImportAsync()
    {
        await JSHost.ImportAsync("BlazorLeafletInterop", "../_content/BlazorLeafletInterop/bundle.js");
    } 
}