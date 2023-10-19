using BlazorLeafletInterop.Models;
using BlazorLeafletInterop.Models.Options.Basic;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Interops;

public class IconFactoryInterop : IAsyncDisposable, IIconFactoryInterop
{
    private const string JsBundleFilePath = "./_content/BlazorLeafletInterop/bundle.js";
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    
    public IconFactoryInterop(IJSRuntime jsRuntime)
    {
        _moduleTask = new Lazy<Task<IJSObjectReference>>(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", JsBundleFilePath).AsTask());
    }
    
    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }

    public async Task<IJSObjectReference> CreateIcon(IconOptions options)
    {
        var jsonOptions = LeafletInterop.ObjectToJson(options);
        var module = await _moduleTask.Value;
        var jsObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", jsonOptions);
        return await module.InvokeAsync<IJSObjectReference>("createIcon", jsObject);
    }

    public async Task<IJSObjectReference> CreateDefaultIcon()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<IJSObjectReference>("createDefaultIcon");
    }
}