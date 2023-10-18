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

namespace BlazorLeafletInterop.Components;

[SupportedOSPlatform("browser")]
public partial class Map
{
    [Parameter]
    public MapOptions MapOptions { get; set; } = new();
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Parameter]
    public string? Class { get; set; }

    public JSObject? MapRef { get; set; }
    public bool IsRendered { get; set; }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (!firstRender) return;
        MapRef = Interop.CreateMap(Id, MapOptions.ToJsObject());
        if (MapRef is null) return;
        IsRendered = true;
        StateHasChanged();
    }

    /// <summary>
    /// Opens the specified popup while closing the previously opened (to make sure only one is opened at one time for usability).
    /// </summary>
    /// <param name="popup"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map OpenPopup(Popup popup)
    {
        if (MapRef is null || popup.PopupRef is null) throw new NullReferenceException("MapRef is null or popup is null");
        Interop.OpenPopup(MapRef, popup.PopupRef);
        return this;
    }
    
    /// <summary>
    /// Closes the popup previously opened with openPopup (or the given one).
    /// </summary>
    /// <param name="popup"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map ClosePopup(Popup popup)
    {
        if (MapRef is null || popup.PopupRef is null) throw new NullReferenceException("MapRef is null or popup is null");
        Interop.ClosePopup(MapRef, popup.PopupRef);
        return this;
    }
    
    /// <summary>
    /// Opens the specified tooltip.
    /// </summary>
    /// <param name="tooltip"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map OpenTooltip(Tooltip tooltip)
    {
        if (MapRef is null || tooltip.TooltipRef is null) throw new NullReferenceException("MapRef is null or tooltip is null");
        Interop.OpenTooltip(MapRef, tooltip.TooltipRef);
        return this;
    }
    
    /// <summary>
    /// Closes the tooltip given as parameter.
    /// </summary>
    /// <param name="tooltip"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map CloseTooltip(Tooltip tooltip)
    {
        if (MapRef is null || tooltip.TooltipRef is null) throw new NullReferenceException("MapRef is null or tooltip is null");
        Interop.CloseTooltip(MapRef, tooltip.TooltipRef);
        return this;
    }
    
    /// <summary>
    /// Sets the view of the map (geographical center and zoom) performing a smooth pan-zoom animation.
    /// </summary>
    /// <param name="latLng"></param>
    /// <param name="zoom"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map FlyTo(LatLng latLng, double zoom)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.FlyTo(MapRef, latLng.ToJsObject(), zoom);
        return this;
    }
    
    /// <summary>
    /// Sets the view of the map with a smooth animation like flyTo, but takes a bounds parameter like fitBounds.
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map FlyToBounds(LatLngBounds bounds, FitBoundsOptions? options)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.FlyToBounds(MapRef, bounds.ToJsObject(), options?.ToJsObject());
        return this;
    }
    
    /// <summary>
    /// Sets a map view that contains the given geographical bounds with the maximum zoom level possible.
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map FitBounds(LatLngBounds bounds, FitBoundsOptions? options)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.FitBounds(MapRef, bounds.ToJsObject(), options?.ToJsObject());
        return this;
    }
    
    /// <summary>
    /// Sets a map view that mostly contains the whole world with the maximum zoom level possible.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map FitWorld(FitBoundsOptions? options)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.FitWorld(MapRef, options?.ToJsObject());
        return this;
    }
    
    /// <summary>
    /// Pans the map to a given center.
    /// </summary>
    /// <param name="latLng"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map PanTo(LatLng latLng, PanOptions? options)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.PanTo(MapRef, latLng.ToJsObject(), options?.ToJsObject());
        return this;
    }
    
    /// <summary>
    /// Pans the map by a given number of pixels (animated).
    /// </summary>
    /// <param name="point"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map PanBy(Point point, PanOptions? options)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.PanBy(MapRef, point.ToJsObject(), options?.ToJsObject());
        return this;
    }
    
    /// <summary>
    /// Restricts the map view to the given bounds (see the maxBounds option).
    /// </summary>
    /// <param name="bounds"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map SetMaxBounds(LatLngBounds bounds)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.SetMaxBounds(MapRef, bounds.ToJsObject());
        return this;
    }
    
    /// <summary>
    /// Sets the lower limit for the available zoom levels (see the minZoom option).
    /// </summary>
    /// <param name="zoom"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map SetMinZoom(double zoom)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.SetMinZoom(MapRef, zoom);
        return this;
    }
    
    /// <summary>
    /// Sets the upper limit for the available zoom levels (see the maxZoom option).
    /// </summary>
    /// <param name="zoom"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map SetMaxZoom(double zoom)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.SetMaxZoom(MapRef, zoom);
        return this;
    }
    
    /// <summary>
    /// Pans the map to the closest view that would lie inside the given bounds (if it's not already), controlling the animation using the options specific, if any.
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map PanInsideBounds(LatLngBounds bounds, PanOptions? options)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.PanInsideBounds(MapRef, bounds.ToJsObject(), options?.ToJsObject());
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
    public Map PanInside(LatLng latLng, PanOptions? options)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.PanInside(MapRef, latLng.ToJsObject(), options?.ToJsObject());
        return this;
    }
    
    /// <summary>
    /// Checks if the map container size changed and updates the map if so
    /// — call it after you've changed the map size dynamically, also animating pan by default.
    /// </summary>
    /// <param name="animate"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map InvalidateSize(bool animate)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.InvalidateSize(MapRef, animate);
        return this;
    }
    
    /// <summary>
    /// Stops the currently running panTo or flyTo animation, if any.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map Stop()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.Stop(MapRef);
        return this;
    }
    
    /// <summary>
    /// Sets the view of the map (geographical center and zoom) with the given animation options.
    /// </summary>
    /// <param name="latLng"></param>
    /// <param name="zoom"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map SetView(LatLng latLng, double zoom)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.SetView(MapRef, latLng.ToJsObject(), zoom);
        return this;
    }
    
    /// <summary>
    /// Sets the zoom of the map.
    /// </summary>
    /// <param name="zoom"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map SetZoom(double zoom)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.SetZoom(MapRef, zoom);
        return this;
    }
    
    /// <summary>
    /// Increases the zoom of the map by delta (zoomDelta by default).
    /// </summary>
    /// <param name="delta"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map ZoomIn(double delta, ZoomOptions? options)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.ZoomIn(MapRef, delta, options?.ToJsObject());
        return this;
    }
    
    /// <summary>
    /// Decreases the zoom of the map by delta (zoomDelta by default).
    /// </summary>
    /// <param name="delta"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public Map ZoomOut(double delta, ZoomOptions? options)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.ZoomOut(MapRef, delta, options?.ToJsObject());
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
    public Map SetZoomAround(LatLng latLng, double zoom, ZoomOptions? options)
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        Interop.SetZoomAround(MapRef, latLng.ToJsObject(), zoom, options?.ToJsObject());
        return this;
    }
    
    #region Methods for Getting Map State
    
    public LatLng? GetCenter()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var center = Interop.GetCenter(MapRef);
        return JsonSerializer.Deserialize<LatLng>(center);
    }
    
    public double GetZoom()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        return Interop.GetZoom(MapRef);
    }
    
    public LatLngBounds? GetBounds()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var bounds = Interop.GetBounds(MapRef);
        return JsonSerializer.Deserialize<LatLngBounds>(bounds);
    }
    
    public double GetMinZoom()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        return Interop.GetMinZoom(MapRef);
    }
    
    public double GetMaxZoom()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        return Interop.GetMaxZoom(MapRef);
    }
    
    public Point? GetSize()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var size = Interop.GetSize(MapRef);
        return JsonSerializer.Deserialize<Point>(size);
    }
    
    public Point? GetPixelOrigin()
    {
        if (MapRef is null) throw new NullReferenceException("MapRef is null");
        var pixelOrigin = Interop.GetPixelOrigin(MapRef);
        return JsonSerializer.Deserialize<Point>(pixelOrigin);
    }
    
    #endregion

    [SupportedOSPlatform("browser")]
    public partial class Interop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static Interop() {}
        
        [JSImport("createMap", "BlazorLeafletInterop")]
        public static partial JSObject CreateMap(string id, JSObject options);

        #region Method for Layers and Controls
        
        [JSImport("addControl", "BlazorLeafletInterop")]
        public static partial JSObject AddControl(JSObject map, JSObject control);
        
        [JSImport("removeControl", "BlazorLeafletInterop")]
        public static partial JSObject RemoveControl(JSObject map, JSObject control);
        
        [JSImport("addLayer", "BlazorLeafletInterop")]
        public static partial JSObject AddLayer(JSObject map, JSObject layer);
        
        [JSImport("removeLayer", "BlazorLeafletInterop")]
        public static partial JSObject RemoveLayer(JSObject map, JSObject layer);
        
        [JSImport("hasLayer", "BlazorLeafletInterop")]
        public static partial bool HasLayer(JSObject map, JSObject layer);
        
        [JSImport("eachLayer", "BlazorLeafletInterop")]
        public static partial JSObject EachLayer(JSObject map, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> fn, JSObject? context);
        
        [JSImport("openMapPopup", "BlazorLeafletInterop")]
        public static partial JSObject OpenPopup(JSObject map, JSObject popup);
        
        [JSImport("closeMapPopup", "BlazorLeafletInterop")]
        public static partial JSObject ClosePopup(JSObject map, JSObject? popup);
        
        [JSImport("openMapTooltip", "BlazorLeafletInterop")]
        public static partial JSObject OpenTooltip(JSObject map, JSObject tooltip);
        
        [JSImport("closeMapTooltip", "BlazorLeafletInterop")]
        public static partial JSObject CloseTooltip(JSObject map, JSObject tooltip);
        
        #endregion
        
        #region Methods for modifiying map state
        
        [JSImport("setView", "BlazorLeafletInterop")]
        public static partial JSObject SetView(JSObject map, JSObject latLng, double zoom);
        
        [JSImport("setZoom", "BlazorLeafletInterop")]
        public static partial JSObject SetZoom(JSObject map, double zoom);
        
        [JSImport("zoomIn", "BlazorLeafletInterop")]
        public static partial JSObject ZoomIn(JSObject map, double delta, JSObject? options);
        
        [JSImport("zoomOut", "BlazorLeafletInterop")]
        public static partial JSObject ZoomOut(JSObject map, double delta, JSObject? options);
        
        [JSImport("setZoomAround", "BlazorLeafletInterop")]
        public static partial JSObject SetZoomAround(JSObject map, JSObject latLng, double zoom, JSObject? options);
        
        [JSImport("fitBounds", "BlazorLeafletInterop")]
        public static partial JSObject FitBounds(JSObject map, JSObject bounds, JSObject? options);
        
        [JSImport("fitWorld", "BlazorLeafletInterop")]
        public static partial JSObject FitWorld(JSObject map, JSObject? options);
        
        [JSImport("panTo", "BlazorLeafletInterop")]
        public static partial JSObject PanTo(JSObject map, JSObject latLng, JSObject? options);
        
        [JSImport("panBy", "BlazorLeafletInterop")]
        public static partial JSObject PanBy(JSObject map, JSObject point, JSObject? options);
        
        [JSImport("flyTo", "BlazorLeafletInterop")]
        public static partial JSObject FlyTo(JSObject map, JSObject latLng, double zoom);
        
        [JSImport("flyToBounds", "BlazorLeafletInterop")]
        public static partial JSObject FlyToBounds(JSObject map, JSObject bounds, JSObject? options);
        
        [JSImport("setMaxBounds", "BlazorLeafletInterop")]
        public static partial JSObject SetMaxBounds(JSObject map, JSObject bounds);
        
        [JSImport("setMinZoom", "BlazorLeafletInterop")]
        public static partial JSObject SetMinZoom(JSObject map, double zoom);
        
        [JSImport("setMaxZoom", "BlazorLeafletInterop")]
        public static partial JSObject SetMaxZoom(JSObject map, double zoom);
        
        [JSImport("panInsideBounds", "BlazorLeafletInterop")]
        public static partial JSObject PanInsideBounds(JSObject map, JSObject bounds, JSObject? options);
        
        [JSImport("panInside", "BlazorLeafletInterop")]
        public static partial JSObject PanInside(JSObject map, JSObject latLng, JSObject? options);
        
        [JSImport("invalidateSize", "BlazorLeafletInterop")]
        public static partial JSObject InvalidateSize(JSObject map, bool animate);
        
        [JSImport("stop", "BlazorLeafletInterop")]
        public static partial JSObject Stop(JSObject map);
        
        #endregion
        
        #region Methods for Getting Map State
        
        [JSImport("getCenter", "BlazorLeafletInterop")]
        public static partial string GetCenter(JSObject map);
            
        [JSImport("getZoom", "BlazorLeafletInterop")]
        public static partial double GetZoom(JSObject map);
        
        [JSImport("getBounds", "BlazorLeafletInterop")]
        public static partial string GetBounds(JSObject map);
        
        [JSImport("getMinZoom", "BlazorLeafletInterop")]
        public static partial double GetMinZoom(JSObject map);
        
        [JSImport("getMaxZoom", "BlazorLeafletInterop")]
        public static partial double GetMaxZoom(JSObject map);
        
        [JSImport("getBoundsZoom", "BlazorLeafletInterop")]
        public static partial double GetBoundsZoom(JSObject map, JSObject bounds, bool? inside, JSObject? padding);
        
        [JSImport("getSize", "BlazorLeafletInterop")]
        public static partial string GetSize(JSObject map);
        
        [JSImport("getPixelBounds", "BlazorLeafletInterop")]
        public static partial string GetPixelBounds(JSObject map);
        
        [JSImport("getPixelOrigin", "BlazorLeafletInterop")]
        public static partial string GetPixelOrigin(JSObject map);
        
        [JSImport("getPixelWorldBounds", "BlazorLeafletInterop")]
        public static partial string GetPixelWorldBounds(JSObject map, int? zoom);
        
        #endregion
    }
}