using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models.Options.Basic;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Factories;

public class IconFactory : IIconFactory
{
    
    private readonly IBundleInterop _bundleInterop;
    private IJSObjectReference? _module;
    
    public IconFactory(IBundleInterop bundleInterop)
    {
        _bundleInterop = bundleInterop;
    }
    
    public async Task<IJSObjectReference> CreateIcon(IconOptions options)
    {
        _module ??= await _bundleInterop.GetModule();
        var jsonOptions = LeafletInterop.ObjectToJson(options);
        var jsObject = await _module.InvokeAsync<IJSObjectReference>("jsonToJsObject", jsonOptions);
        return await _module.InvokeAsync<IJSObjectReference>("createIcon", jsObject);
    }

    public async Task<IJSObjectReference> CreateDefaultIcon()
    {
        _module ??= await _bundleInterop.GetModule();
        return await _module.InvokeAsync<IJSObjectReference>("createDefaultIcon");
    }
}