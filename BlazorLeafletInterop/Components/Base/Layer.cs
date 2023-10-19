using BlazorLeafletInterop.Models;
using BlazorLeafletInterop.Models.Basics;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Base;

public class Layer : Evented
{
    public LayerOptions LayerOptions { get; set; } = new();

    public async Task<T> AddTo<T>(IJSObjectReference? map, IJSObjectReference? @ref)
    {
        if (@ref is null || map is null) throw new NullReferenceException("Ref or map is null");
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("addTo", @ref, map);
        return (T)(object)this;
    }

    public async Task<T> RemoveFrom<T>(IJSObjectReference? map, IJSObjectReference? @ref)
    {
        if (@ref is null || map is null) throw new NullReferenceException("Ref or map is null");
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("removeFrom", @ref, map);
        return (T)(object)this;
    }

    public async Task<T> Remove<T>(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("remove", @ref);
        return (T)(object)this;
    }
    
    public async Task<IJSObjectReference> GetPane(string? name, IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var paneRef = await module.InvokeAsync<IJSObjectReference>("getPane", @ref, name);
        return paneRef;
    }
    
    public async Task<IJSObjectReference> GetAttribution(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var attributionRef = await module.InvokeAsync<IJSObjectReference>("getAttribution", @ref);
        return attributionRef;
    }
    
    public async Task<IJSObjectReference> BindPopup(IJSObjectReference? @ref, string content, PopupOptions? options)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var popupRef = await module.InvokeAsync<IJSObjectReference>("bindPopup", @ref, content, options);
        return popupRef;
    }
    
    public async Task<IJSObjectReference> UnbindPopup(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var popupRef = await module.InvokeAsync<IJSObjectReference>("unbindPopup", @ref);
        return popupRef;
    }
    
    public async Task<IJSObjectReference> OpenPopup(IJSObjectReference? @ref, LatLng? latLng)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var popupRef = await module.InvokeAsync<IJSObjectReference>("openPopup", @ref, latLng);
        return popupRef;
    }
    
    public async Task<IJSObjectReference> ClosePopup(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var popupRef = await module.InvokeAsync<IJSObjectReference>("closePopup", @ref);
        return popupRef;
    }
    
    public async Task<IJSObjectReference> TogglePopup(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var popupRef = await module.InvokeAsync<IJSObjectReference>("togglePopup", @ref);
        return popupRef;
    }
    
    public async Task<bool> IsPopupOpen(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var isOpen = await module.InvokeAsync<bool>("isPopupOpen", @ref);
        return isOpen;
    }
    
    public async Task<IJSObjectReference> SetPopupContent(IJSObjectReference? @ref, string content)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var popupRef = await module.InvokeAsync<IJSObjectReference>("setPopupContent", @ref, content);
        return popupRef;
    }
    
    public async Task<IJSObjectReference> GetPopup(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var popupRef = await module.InvokeAsync<IJSObjectReference>("getPopup", @ref);
        return popupRef;
    }
    
    public async Task<IJSObjectReference> BindTooltip(IJSObjectReference? @ref, string content, TooltipOptions? options)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var tooltipRef = await module.InvokeAsync<IJSObjectReference>("bindTooltip", @ref, content, options);
        return tooltipRef;
    }
    
    public async Task<IJSObjectReference> UnbindTooltip(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var tooltipRef = await module.InvokeAsync<IJSObjectReference>("unbindTooltip", @ref);
        return tooltipRef;
    }
    
    public async Task<IJSObjectReference> OpenTooltip(IJSObjectReference? @ref, LatLng? latLng)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var tooltipRef = await module.InvokeAsync<IJSObjectReference>("openTooltip", @ref, latLng);
        return tooltipRef;
    }
    
    public async Task<IJSObjectReference> CloseTooltip(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var tooltipRef = await module.InvokeAsync<IJSObjectReference>("closeTooltip", @ref);
        return tooltipRef;
    }
    
    public async Task<IJSObjectReference> ToggleTooltip(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var tooltipRef = await module.InvokeAsync<IJSObjectReference>("toggleTooltip", @ref);
        return tooltipRef;
    }
    
    public async Task<bool> IsTooltipOpen(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var isOpen = await module.InvokeAsync<bool>("isTooltipOpen", @ref);
        return isOpen;
    }
    
    public async Task<IJSObjectReference> SetTooltipContent(IJSObjectReference? @ref, string content)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var tooltipRef = await module.InvokeAsync<IJSObjectReference>("setTooltipContent", @ref, content);
        return tooltipRef;
    }
    
    public async Task<IJSObjectReference> GetTooltip(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        var module = await BundleInterop.GetModule();
        var tooltipRef = await module.InvokeAsync<IJSObjectReference>("getTooltip", @ref);
        return tooltipRef;
    }
}