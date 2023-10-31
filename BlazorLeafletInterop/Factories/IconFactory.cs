using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models.Options.Basic;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Factories;

public class IconFactory : IIconFactory
{
    
    private readonly IJSRuntime _jsRuntime;
    
    public IconFactory(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    
    public async Task<IJSObjectReference> CreateIcon(IconOptions options)
    {
        return await _jsRuntime.InvokeAsync<IJSObjectReference>("L.icon", options);
    }

    public async Task<IJSObjectReference> CreateDefaultIcon()
    {
        return await _jsRuntime.InvokeAsync<IJSObjectReference>("L.Icon.Default");
    }
}