using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Interops;

public class  BundleInterop : IAsyncDisposable, IBundleInterop
{
    private const string JsBundleFilePath = "./_content/BlazorLeafletInterop/bundle.js";
    private readonly IJSRuntime _jsRuntime;
    private IJSObjectReference? _module;
    
    public BundleInterop(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    
    public async ValueTask DisposeAsync()
    {
        if (_module != null) await _module.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public async Task<IJSObjectReference> GetModule()
    {
        return _module ??= await _jsRuntime.InvokeAsync<IJSObjectReference>("import", JsBundleFilePath);
    }
}