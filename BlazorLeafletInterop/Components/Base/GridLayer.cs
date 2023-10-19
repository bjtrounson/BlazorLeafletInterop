using System.Runtime.Versioning;
using BlazorLeafletInterop.Models;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Base;

[SupportedOSPlatform("browser")]
public class GridLayer : Layer
{
    public GridLayerOptions GridLayerOptions { get; set; } = new();

    public async Task<T> BringToFront<T>(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("bringToFront", @ref);
        return (T)(object)this;
    }
    
    public async Task<T> BringToBack<T>(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("bringToBack", @ref);
        return (T)(object)this;
    }
    
    public async Task<IJSObjectReference> GetContainer(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var containerRef = await module.InvokeAsync<IJSObjectReference>("getContainer", @ref);
        return containerRef;
    }
    
    public async Task<T> SetOpacity<T>(IJSObjectReference? @ref, double opacity)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("setOpacity", @ref, opacity);
        return (T)(object)this;
    }
    
    public async Task<T> SetZIndex<T>(IJSObjectReference? @ref, int zIndex)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("setZIndex", @ref, zIndex);
        return (T)(object)this;
    }
    
    public async Task<bool> IsLoading(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var isLoading = await module.InvokeAsync<bool>("isLoading", @ref);
        return isLoading;
    }
    
    public async Task<T> Redraw<T>(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("redraw", @ref);
        return (T)(object)this;
    }
    
    public async Task<IJSObjectReference> GetTileSize(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var tileSizeRef = await module.InvokeAsync<IJSObjectReference>("getTileSize", @ref);
        return tileSizeRef;
    }
}