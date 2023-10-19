using BlazorLeafletInterop.Components.Base;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models.Options.Layer.Vector;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Layers.Vector;

public class Path : Layer
{
    public PathOptions PathOptions { get; set; } = new();

    public async Task<T> Redraw<T>(IJSObjectReference @ref)
    {
        if (@ref is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("redraw", @ref);
        return (T)(object)this;
    }

    public async Task<T> SetStyle<T>(IJSObjectReference @ref, PathOptions pathOptions)
    {
        if (@ref is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        var pathOptionsJson = LeafletInterop.ObjectToJson(pathOptions);
        var pathOptionsObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", pathOptionsJson);
        await module.InvokeVoidAsync("setStyle", @ref, pathOptionsObject);
        return (T)(object)this;
    }
    
    public async Task<T> BringToFront<T>(IJSObjectReference @ref)
    {
        if (@ref is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("bringToFront", @ref);
        return (T)(object)this;
    }
    
    public async Task<T> BringToBack<T>(IJSObjectReference @ref)
    {
        if (@ref is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("bringToBack", @ref);
        return (T)(object)this;
    }
}