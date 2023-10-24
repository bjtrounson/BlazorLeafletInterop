using BlazorLeafletInterop.Components.Base;
using BlazorLeafletInterop.Models.Options.Layer.UI;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Layers.UI;

public class DivOverlay : InteractiveLayer
{
    public DivOverlayOptions DivOverlayOptions { get; set; } = new();
    
    public async Task<IJSObjectReference> OpenOn(IJSObjectReference? map, IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("openOn", @ref, map);
    }
    
    public async Task<IJSObjectReference> Close(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("close", @ref);
    }
    
    public async Task<IJSObjectReference> Toggle(IJSObjectReference? layer, IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("toggle", @ref, layer);
    }
    
    public async Task<IJSObjectReference> GetLatLng(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("getLatLng", @ref);
    }
    
    public async Task<IJSObjectReference> SetLatLng(IJSObjectReference latLng, IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("setLatLng", @ref, latLng);
    }
    
    public async Task<string> GetContent(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<string>("getContent", @ref);
    }
    
    public async Task<IJSObjectReference> SetContent(string content, IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("setContent", @ref, content);
    }
    
    public async Task<string> GetElement(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<string>("getElement", @ref);
    }
    
    public async Task Update(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("update", @ref);
    }
    
    public async Task<bool> IsOpen(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<bool>("isOpen", @ref);
    }
    
    public async Task<IJSObjectReference> BringToFront(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("bringToFront", @ref);
    }
    
    public async Task<IJSObjectReference> BringToBack(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("bringToBack", @ref);
    }
}