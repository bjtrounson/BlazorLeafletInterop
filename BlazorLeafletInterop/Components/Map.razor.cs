using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Components.Base;
using BlazorLeafletInterop.Components.Layers.UI;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models;
using BlazorLeafletInterop.Models.Basics;
using BlazorLeafletInterop.Models.Options.Map;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components;

public partial class Map : IAsyncDisposable
{
    [Parameter]
    public MapOptions MapOptions { get; set; } = new();
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Parameter]
    public string? Class { get; set; }
    
    [Parameter]
    public Action<IJSObjectReference> EachLayerCallback { get; set; } = default!;
    
    [Parameter]
    public EventCallback AfterRender { get; set; }
    
    public IJSObjectReference? MapRef { get; set; }
    public bool IsRendered { get; set; }
    
    public DotNetObjectReference<Map>? MapRefDotNetObjectRef { get; set; }
    
    [JSInvokable]
    public void EachLayerDotNetToJs(IJSObjectReference layer) => EachLayerCallback(layer);

    protected override void OnInitialized()
    {
        MapRefDotNetObjectRef = DotNetObjectReference.Create(this);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (!firstRender) return;
        MapRef = await CreateMap(Id, MapOptions).ConfigureAwait(false);
        if (MapRef is null) return;
        IsRendered = true;
        StateHasChanged();
        await AfterRender.InvokeAsync();
    }

    private async Task<IJSObjectReference> CreateMap(string id, MapOptions options)
    {
        Module ??= await BundleInterop.GetModule();
        var mapOptionsJson = LeafletInterop.ObjectToJson(options);
        var mapOptionsObject = await Module.InvokeAsync<IJSObjectReference>("jsonToJsObject", mapOptionsJson);
        return await Module.InvokeAsync<IJSObjectReference>("createMap", id, mapOptionsObject);
    }

    /// <summary>
    /// Iterates over the layers of the map, optionally specifying context of the iterator function.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> EachLayer(object? context)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        await Module.InvokeVoidAsync("eachLayer", MapRefDotNetObjectRef, "EachLayerDotNetToJs", MapRef, context);
        return this;
    }

    /// <summary>
    /// Opens the specified popup while closing the previously opened (to make sure only one is opened at one time for usability).
    /// </summary>
    /// <param name="popup"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> OpenPopup(Popup popup)
    {
        if (MapRef is null || popup.PopupRef is null || Module is null) throw new NullReferenceException("MapRef, popup or bundle module is null");
        await Module.InvokeVoidAsync("openMapPopup", MapRef, popup.PopupRef);
        return this;
    }
    
    /// <summary>
    /// Closes the popup previously opened with openPopup (or the given one).
    /// </summary>
    /// <param name="popup"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> ClosePopup(Popup popup)
    {
        if (MapRef is null || popup.PopupRef is null || Module is null) throw new NullReferenceException("MapRef, popup or bundle module is null");
        await Module.InvokeVoidAsync("closeMapPopup", MapRef, popup.PopupRef);
        return this;
    }
    
    /// <summary>
    /// Opens the specified tooltip.
    /// </summary>
    /// <param name="tooltip"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> OpenTooltip(Tooltip tooltip)
    {
        if (MapRef is null || tooltip.TooltipRef is null || Module is null) throw new NullReferenceException("MapRef, tooltip or bundle module is null");
        await Module.InvokeVoidAsync("openMapTooltip", MapRef, tooltip.TooltipRef);
        return this;
    }
    
    /// <summary>
    /// Closes the tooltip given as parameter.
    /// </summary>
    /// <param name="tooltip"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> CloseTooltip(Tooltip tooltip)
    {
        if (MapRef is null || tooltip.TooltipRef is null || Module is null) throw new NullReferenceException("MapRef, tooltip or bundle module is null");
        await Module.InvokeVoidAsync("closeMapTooltip", MapRef, tooltip.TooltipRef);
        return this;
    }
    
    /// <summary>
    /// Sets the view of the map (geographical center and zoom) performing a smooth pan-zoom animation.
    /// </summary>
    /// <param name="latLng"></param>
    /// <param name="zoom"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> FlyTo(LatLng latLng, double zoom)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        await Module.InvokeVoidAsync("flyTo", MapRef, latLng, zoom);
        return this;
    }
    
    /// <summary>
    /// Sets the view of the map with a smooth animation like flyTo, but takes a bounds parameter like fitBounds.
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> FlyToBounds(LatLngBounds bounds, FitBoundsOptions? options)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        if (options is null)
        {
            await Module.InvokeVoidAsync("flyToBounds", MapRef, bounds);
            return this;
        }
        await Module.InvokeVoidAsync("flyToBounds", MapRef, bounds, options);
        return this;
    }
    
    /// <summary>
    /// Sets a map view that contains the given geographical bounds with the maximum zoom level possible.
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> FitBounds(LatLngBounds bounds, FitBoundsOptions? options)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        if (options is null)
        {
            await Module.InvokeVoidAsync("fitBounds", MapRef, bounds);
            return this;
        }
        await Module.InvokeVoidAsync("fitBounds", MapRef, bounds, options);
        return this;
    }
    
    /// <summary>
    /// Sets a map view that mostly contains the whole world with the maximum zoom level possible.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> FitWorld(FitBoundsOptions? options)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        if (options is null)
        {
            await Module.InvokeVoidAsync("fitWorld", MapRef);
            return this;
        }
        await Module.InvokeVoidAsync("fitWorld", MapRef, options);
        return this;
    }
    
    /// <summary>
    /// Pans the map to a given center.
    /// </summary>
    /// <param name="latLng"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> PanTo(LatLng latLng, PanOptions? options)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        if (options is null)
        {
            await Module.InvokeVoidAsync("panTo", MapRef, latLng);
            return this;
        }
        await Module.InvokeVoidAsync("panTo", MapRef, latLng, options);
        return this;
    }
    
    /// <summary>
    /// Pans the map by a given number of pixels (animated).
    /// </summary>
    /// <param name="point"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> PanBy(Point point, PanOptions? options)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        if (options is null)
        {
            await Module.InvokeVoidAsync("panBy", MapRef, point);
            return this;
        }
        await Module.InvokeVoidAsync("panBy", MapRef, point, options);
        return this;
    }
    
    /// <summary>
    /// Restricts the map view to the given bounds (see the maxBounds option).
    /// </summary>
    /// <param name="bounds"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> SetMaxBounds(LatLngBounds bounds)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        await Module.InvokeVoidAsync("setMaxBounds", MapRef, bounds);
        return this;
    }
    
    /// <summary>
    /// Sets the lower limit for the available zoom levels (see the minZoom option).
    /// </summary>
    /// <param name="zoom"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> SetMinZoom(double zoom)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        await Module.InvokeVoidAsync("setMinZoom", MapRef, zoom);
        return this;
    }
    
    /// <summary>
    /// Sets the upper limit for the available zoom levels (see the maxZoom option).
    /// </summary>
    /// <param name="zoom"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> SetMaxZoom(double zoom)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        await Module.InvokeVoidAsync("setMaxZoom", MapRef, zoom);
        return this;
    }
    
    /// <summary>
    /// Pans the map to the closest view that would lie inside the given bounds (if it's not already), controlling the animation using the options specific, if any.
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> PanInsideBounds(LatLngBounds bounds, PanOptions? options)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        if (options is null)
        {
            await Module.InvokeVoidAsync("panInsideBounds", MapRef, bounds);
            return this;
        }
        await Module.InvokeVoidAsync("panInsideBounds", MapRef, bounds, options);
        return this;
    }
    
    /// <summary>
    /// Pans the map the minimum amount to make the latLng visible.
    /// Use either the offset option or provide padding in the form of [topLeftPoint, bottomRightPoint],
    /// where both topLeftPoint and bottomRightPoint are points.
    /// </summary>
    /// <param name="latLng"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> PanInside(LatLng latLng, PanOptions? options)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        if (options is null)
        {
            await Module.InvokeVoidAsync("panInside", MapRef, latLng);
            return this;
        }
        await Module.InvokeVoidAsync("panInside", MapRef, latLng, options);
        return this;
    }
    
    /// <summary>
    /// Checks if the map container size changed and updates the map if so
    /// — call it after you've changed the map size dynamically, also animating pan by default.
    /// </summary>
    /// <param name="animate"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> InvalidateSize(bool animate)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        await Module.InvokeVoidAsync("invalidateSize", MapRef, animate);
        return this;
    }
    
    /// <summary>
    /// Stops the currently running panTo or flyTo animation, if any.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> Stop()
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        await Module.InvokeVoidAsync("stop", MapRef);
        return this;
    }
    
    /// <summary>
    /// Sets the view of the map (geographical center and zoom) with the given animation options.
    /// </summary>
    /// <param name="latLng"></param>
    /// <param name="zoom"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> SetView(LatLng latLng, double zoom)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        await Module.InvokeVoidAsync("setView", MapRef, latLng, zoom);
        return this;
    }
    
    /// <summary>
    /// Sets the zoom of the map.
    /// </summary>
    /// <param name="zoom"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> SetZoom(double zoom)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        await Module.InvokeVoidAsync("setZoom", MapRef, zoom);
        return this;
    }
    
    /// <summary>
    /// Increases the zoom of the map by delta (zoomDelta by default).
    /// </summary>
    /// <param name="delta"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> ZoomIn(double delta, ZoomOptions? options)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        if (options is null)
        {
            await Module.InvokeVoidAsync("zoomIn", MapRef, delta);
            return this;
        }
        await Module.InvokeVoidAsync("zoomIn", MapRef, delta, options);
        return this;
    }
    
    /// <summary>
    /// Decreases the zoom of the map by delta (zoomDelta by default).
    /// </summary>
    /// <param name="delta"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> ZoomOut(double delta, ZoomOptions? options)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        if (options is null)
        {
            await Module.InvokeVoidAsync("zoomOut", MapRef, delta);
            return this;
        }
        await Module.InvokeVoidAsync("zoomOut", MapRef, delta, options);
        return this;
    }
    
    /// <summary>
    /// Zooms the map while keeping a specified geographical point on the map stationary (e.g. used internally for scroll zoom and double-click zoom).
    /// </summary>
    /// <param name="latLng"></param>
    /// <param name="zoom"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> SetZoomAround(LatLng latLng, double zoom, ZoomOptions? options)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        if (options is null)
        {
            await Module.InvokeVoidAsync("setZoomAround", MapRef, latLng, zoom);
            return this;
        }
        await Module.InvokeVoidAsync("setZoomAround", MapRef, latLng, zoom, options);
        return this;
    }
    
    #region Methods for Getting Map State
    
    /// <summary>
    /// Returns the geographical center of the map view.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<LatLng?> GetCenter()
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        var center = await Module.InvokeAsync<string>("getCenter", MapRef);
        return JsonSerializer.Deserialize<LatLng>(center);
    }
    
    /// <summary>
    /// Returns the current zoom of the map view.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<double> GetZoom()
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        return await Module.InvokeAsync<double>("getZoom", MapRef);
    }
    
    /// <summary>
    /// Returns the minimum zoom level of the map.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<LatLngBounds?> GetBounds()
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        var bounds = await Module.InvokeAsync<string>("getBounds", MapRef);
        return JsonSerializer.Deserialize<LatLngBounds>(bounds);
    }
    
    /// <summary>
    /// Returns the maximum zoom level of the map.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<double> GetMinZoom()
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        return await Module.InvokeAsync<double>("getMinZoom", MapRef);
    }
    
    /// <summary>
    /// Returns the maximum zoom level of the map.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<double> GetMaxZoom()
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        return await Module.InvokeAsync<double>("getMaxZoom", MapRef);
    }
    
    /// <summary>
    /// Returns the current size of the map container.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Point?> GetSize()
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        var size = await Module.InvokeAsync<string>("getSize", MapRef);
        return JsonSerializer.Deserialize<Point>(size);
    }
    
    /// <summary>
    /// Returns the bounds of the current map view in projected pixel coordinates (sometimes useful in layer and overlay implementations).
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Point?> GetPixelOrigin()
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        var pixelOrigin = await Module.InvokeAsync<string>("getPixelOrigin", MapRef);
        return JsonSerializer.Deserialize<Point>(pixelOrigin);
    }
    
    #endregion

    /// <summary>
    /// Removes the given layer to the map.
    /// </summary>
    /// <param name="layer"></param>
    public async Task RemoveLayer(IJSObjectReference layer)
    {
        if (MapRef is null || Module is null) throw new NullReferenceException("MapRef or Module is null");
        await Module.InvokeVoidAsync("removeLayer", MapRef, layer);
    }

    public async ValueTask DisposeAsync()
    {
        if (MapRef is null) return;
        async void LayerCallback(IJSObjectReference layer) => await RemoveLayer(layer);
        EachLayerCallback = LayerCallback;
        await EachLayer(null);
        MapRefDotNetObjectRef?.Dispose();
        GC.SuppressFinalize(this);
    }
}