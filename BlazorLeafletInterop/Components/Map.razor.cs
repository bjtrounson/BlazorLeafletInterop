using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Components.Base;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models;
using BlazorLeafletInterop.Models.Basics;
using BlazorLeafletInterop.Models.Map;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components;

[SupportedOSPlatform("browser")]
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
    }

    private async Task<IJSObjectReference> CreateMap(string id, MapOptions options)
    {
        var bundleModule = await BundleInterop.GetModule();
        var mapOptionsJson = LeafletInterop.ObjectToJson(options);
        var mapOptionsObject = await bundleModule.InvokeAsync<IJSObjectReference>("jsonToJsObject", mapOptionsJson);
        return await bundleModule.InvokeAsync<IJSObjectReference>("createMap", id, mapOptionsObject);
    }

    public async Task<Map> EachLayer(object? context)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("eachLayer", MapRefDotNetObjectRef, "EachLayerDotNetToJs", MapRef, context);
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
        if (MapRef is null || popup.PopupRef is null) throw new NullReferenceException("MapRef is null or popup is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("openMapPopup", MapRef, popup.PopupRef);
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
        if (MapRef is null || popup.PopupRef is null) throw new NullReferenceException("MapRef is null or popup is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("closeMapPopup", MapRef, popup.PopupRef);
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
        if (MapRef is null || tooltip.TooltipRef is null) throw new NullReferenceException("MapRef is null or tooltip is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("openMapTooltip", MapRef, tooltip.TooltipRef);
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
        if (MapRef is null || tooltip.TooltipRef is null) throw new NullReferenceException("MapRef is null or tooltip is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("closeMapTooltip", MapRef, tooltip.TooltipRef);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("flyTo", MapRef, latLng, zoom);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("flyToBounds", MapRef, bounds, options);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("fitBounds", MapRef, bounds, options);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("fitWorld", MapRef, options);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("panTo", MapRef, latLng, options);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("panBy", MapRef, point, options);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("setMaxBounds", MapRef, bounds);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("setMinZoom", MapRef, zoom);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("setMaxZoom", MapRef, zoom);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("panInsideBounds", MapRef, bounds, options);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("panInside", MapRef, latLng, options);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("invalidateSize", MapRef, animate);
        return this;
    }
    
    /// <summary>
    /// Stops the currently running panTo or flyTo animation, if any.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<Map> Stop()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("stop", MapRef);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("setView", MapRef, latLng, zoom);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("setZoom", MapRef, zoom);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("zoomIn", MapRef, delta, options);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("zoomOut", MapRef, delta, options);
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
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("setZoomAround", MapRef, latLng, zoom, options);
        return this;
    }
    
    #region Methods for Getting Map State
    
    public async Task<LatLng?> GetCenter()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        var center = await bundleModule.InvokeAsync<string>("getCenter", MapRef);
        return JsonSerializer.Deserialize<LatLng>(center);
    }
    
    public async Task<double> GetZoom()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        return await bundleModule.InvokeAsync<double>("getZoom", MapRef);
    }
    
    public async Task<LatLngBounds?> GetBounds()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        var bounds = await bundleModule.InvokeAsync<string>("getBounds", MapRef);
        return JsonSerializer.Deserialize<LatLngBounds>(bounds);
    }
    
    public async Task<double> GetMinZoom()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        return await bundleModule.InvokeAsync<double>("getMinZoom", MapRef);
    }
    
    public async Task<double> GetMaxZoom()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        return await bundleModule.InvokeAsync<double>("getMaxZoom", MapRef);
    }
    
    public async Task<Point?> GetSize()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        var size = await bundleModule.InvokeAsync<string>("getSize", MapRef);
        return JsonSerializer.Deserialize<Point>(size);
    }
    
    public async Task<Point?> GetPixelOrigin()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bundleModule = await BundleInterop.GetModule();
        var pixelOrigin = await bundleModule.InvokeAsync<string>("getPixelOrigin", MapRef);
        return JsonSerializer.Deserialize<Point>(pixelOrigin);
    }
    
    #endregion

    public async Task RemoveLayer(IJSObjectReference layer)
    {
        var bundleModule = await BundleInterop.GetModule();
        await bundleModule.InvokeVoidAsync("removeLayer", MapRef, layer);
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