using BlazorLeafletInterop.Components.Base;
using BlazorLeafletInterop.Models.Options.Layer.Misc;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Layers.Misc;

public class GridLayer : Layer
{
    public GridLayerOptions GridLayerOptions { get; set; } = new();

    public async Task<T> BringToFront<T>(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("bringToFront", @ref);
        return (T)(object)this;
    }
    
    public async Task<T> BringToBack<T>(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("bringToBack", @ref);
        return (T)(object)this;
    }
    
    public async Task<IJSObjectReference> GetContainer(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var containerRef = await Module.InvokeAsync<IJSObjectReference>("getContainer", @ref);
        return containerRef;
    }
    
    public async Task<T> SetOpacity<T>(IJSObjectReference? @ref, double opacity)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("setOpacity", @ref, opacity);
        return (T)(object)this;
    }
    
    public async Task<T> SetZIndex<T>(IJSObjectReference? @ref, int zIndex)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("setZIndex", @ref, zIndex);
        return (T)(object)this;
    }
    
    public async Task<bool> IsLoading(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var isLoading = await Module.InvokeAsync<bool>("isLoading", @ref);
        return isLoading;
    }
    
    public async Task<T> Redraw<T>(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("redraw", @ref);
        return (T)(object)this;
    }
    
    public async Task<IJSObjectReference> GetTileSize(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var tileSizeRef = await Module.InvokeAsync<IJSObjectReference>("getTileSize", @ref);
        return tileSizeRef;
    }
}