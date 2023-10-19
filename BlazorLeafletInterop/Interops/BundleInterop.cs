using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Interops;

public class BundleInterop : IAsyncDisposable, IBundleInterop
{
    private const string JsBundleFilePath = "./_content/BlazorLeafletInterop/bundle.js";
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    
    public BundleInterop(IJSRuntime jsRuntime)
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

    public async Task<IJSObjectReference> GetModule()
    {
        return await _moduleTask.Value;
    }
}