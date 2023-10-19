using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Interops;

public interface IBundleInterop
{
    ValueTask DisposeAsync();
    Task<IJSObjectReference> GetModule();
}