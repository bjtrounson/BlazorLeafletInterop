using BlazorLeafletInterop.Models.Basics;
using BlazorLeafletInterop.Models.Options.Base;
using BlazorLeafletInterop.Models.Options.Layer.UI;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Base;

public class Layer : Evented
{
    public LayerOptions LayerOptions { get; set; } = new();

    /// <summary>
    /// Adds the layer to the given map or layer group.
    /// </summary>
    /// <param name="map"></param>
    /// <param name="ref"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<T> AddTo<T>(IJSObjectReference? map, IJSObjectReference? @ref)
    {
        if (@ref is null || map is null) throw new NullReferenceException("Ref or map is null");
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("addTo", @ref, map);
        return (T)(object)this;
    }

    /// <summary>
    /// Removes the layer from the given map or layer group.
    /// </summary>
    /// <param name="map"></param>
    /// <param name="ref"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<T> RemoveFrom<T>(IJSObjectReference? map, IJSObjectReference? @ref)
    {
        if (@ref is null || map is null) throw new NullReferenceException("Ref or map is null");
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("removeFrom", @ref, map);
        return (T)(object)this;
    }

    /// <summary>
    /// Removes the layer from the map it is currently active on.
    /// </summary>
    /// <param name="ref"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<T> Remove<T>(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("remove", @ref);
        return (T)(object)this;
    }
    
    /// <summary>
    /// Returns the HTMLElement representing the named pane on the map. If name is omitted, returns the pane for this layer.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ref"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> GetPane(string? name, IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var paneRef = await Module.InvokeAsync<IJSObjectReference>("getPane", @ref, name);
        return paneRef;
    }
    
    /// <summary>
    /// Used by the attribution control, returns the attribution option.
    /// </summary>
    /// <param name="ref"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> GetAttribution(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var attributionRef = await Module.InvokeAsync<IJSObjectReference>("getAttribution", @ref);
        return attributionRef;
    }
    
    /// <summary>
    /// Binds a popup to the layer with the passed content and sets up the necessary event listeners.
    /// </summary>
    /// <param name="ref"></param>
    /// <param name="content"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> BindPopup(IJSObjectReference? @ref, string content, PopupOptions? options)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var popupRef = await Module.InvokeAsync<IJSObjectReference>("bindPopup", @ref, content, options);
        return popupRef;
    }
    
    /// <summary>
    /// Removes the popup previously bound with bindPopup.
    /// </summary>
    /// <param name="ref"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> UnbindPopup(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var popupRef = await Module.InvokeAsync<IJSObjectReference>("unbindPopup", @ref);
        return popupRef;
    }
    
    /// <summary>
    /// Opens the bound popup at the specified latlng or at the default popup anchor if no latlng is passed.
    /// </summary>
    /// <param name="ref"></param>
    /// <param name="latLng"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> OpenPopup(IJSObjectReference? @ref, LatLng? latLng)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var popupRef = await Module.InvokeAsync<IJSObjectReference>("openPopup", @ref, latLng);
        return popupRef;
    }
    
    /// <summary>
    /// Closes the popup bound to this layer if it is open.
    /// </summary>
    /// <param name="ref"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> ClosePopup(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var popupRef = await Module.InvokeAsync<IJSObjectReference>("closePopup", @ref);
        return popupRef;
    }
    
    /// <summary>
    /// Opens or closes the popup bound to this layer depending on its current state.
    /// </summary>
    /// <param name="ref"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> TogglePopup(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var popupRef = await Module.InvokeAsync<IJSObjectReference>("togglePopup", @ref);
        return popupRef;
    }
    
    /// <summary>
    /// Returns true if the popup bound to this layer is currently open.
    /// </summary>
    /// <param name="ref"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<bool> IsPopupOpen(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var isOpen = await Module.InvokeAsync<bool>("isPopupOpen", @ref);
        return isOpen;
    }
    
    /// <summary>
    /// Sets the content of the popup bound to this layer.
    /// </summary>
    /// <param name="ref"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> SetPopupContent(IJSObjectReference? @ref, string content)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var popupRef = await Module.InvokeAsync<IJSObjectReference>("setPopupContent", @ref, content);
        return popupRef;
    }
    
    /// <summary>
    /// Returns the popup bound to this layer.
    /// </summary>
    /// <param name="ref"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> GetPopup(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var popupRef = await Module.InvokeAsync<IJSObjectReference>("getPopup", @ref);
        return popupRef;
    }
    
    /// <summary>
    /// Binds a tooltip to the layer with the passed content and sets up the necessary event listeners.
    /// </summary>
    /// <param name="ref"></param>
    /// <param name="content"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> BindTooltip(IJSObjectReference? @ref, string content, TooltipOptions? options)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var tooltipRef = await Module.InvokeAsync<IJSObjectReference>("bindTooltip", @ref, content, options);
        return tooltipRef;
    }
    
    /// <summary>
    /// Removes the tooltip previously bound with bindTooltip.
    /// </summary>
    /// <param name="ref"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> UnbindTooltip(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var tooltipRef = await Module.InvokeAsync<IJSObjectReference>("unbindTooltip", @ref);
        return tooltipRef;
    }
    
    /// <summary>
    /// Opens the bound tooltip at the specified latlng or at the default tooltip anchor if no latlng is passed.
    /// </summary>
    /// <param name="ref"></param>
    /// <param name="latLng"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> OpenTooltip(IJSObjectReference? @ref, LatLng? latLng)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var tooltipRef = await Module.InvokeAsync<IJSObjectReference>("openTooltip", @ref, latLng);
        return tooltipRef;
    }
    
    /// <summary>
    /// Closes the tooltip bound to this layer if it is open.
    /// </summary>
    /// <param name="ref"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> CloseTooltip(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var tooltipRef = await Module.InvokeAsync<IJSObjectReference>("closeTooltip", @ref);
        return tooltipRef;
    }
    
    /// <summary>
    /// Opens or closes the tooltip bound to this layer depending on its current state.
    /// </summary>
    /// <param name="ref"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> ToggleTooltip(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var tooltipRef = await Module.InvokeAsync<IJSObjectReference>("toggleTooltip", @ref);
        return tooltipRef;
    }
    
    /// <summary>
    /// Returns true if the tooltip bound to this layer is currently open.
    /// </summary>
    /// <param name="ref"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<bool> IsTooltipOpen(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var isOpen = await Module.InvokeAsync<bool>("isTooltipOpen", @ref);
        return isOpen;
    }
    
    /// <summary>
    /// Sets the content of the tooltip bound to this layer.
    /// </summary>
    /// <param name="ref"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> SetTooltipContent(IJSObjectReference? @ref, string content)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var tooltipRef = await Module.InvokeAsync<IJSObjectReference>("setTooltipContent", @ref, content);
        return tooltipRef;
    }
    
    /// <summary>
    /// Returns the tooltip bound to this layer.
    /// </summary>
    /// <param name="ref"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> GetTooltip(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await BundleInterop.GetModule();
        var tooltipRef = await Module.InvokeAsync<IJSObjectReference>("getTooltip", @ref);
        return tooltipRef;
    }
}