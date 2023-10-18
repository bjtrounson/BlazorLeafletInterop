using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models;
using BlazorLeafletInterop.Models.Basics;
using Microsoft.AspNetCore.Components;

namespace BlazorLeafletInterop.Components;

[SupportedOSPlatform("browser")]
public partial class Marker
{
    [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();
    [Parameter] public LatLng LatLng { get; set; } = new();
    [Parameter] public Icon? Icon { get; set; }
    [Parameter] public MarkerOptions Options { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    [CascadingParameter(Name = "LayerGroup")] public LayerGroup? LayerGroup { get; set; }
    [CascadingParameter(Name = "MapRef")] public JSObject? MapRef { get; set; }
    
    public JSObject? MarkerRef { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (!OperatingSystem.IsBrowser()) throw new PlatformNotSupportedException();
        await base.OnInitializedAsync();
        MarkerRef = CreateMarker(LatLng, Options);
        if (LayerGroup is not null && MarkerRef is not null)
        {
            LayerGroup.AddLayer(MarkerRef);
            return;
        }
        if (MapRef is null || MarkerRef is null) return;
        AddTo(MapRef);
    }
    
    public Marker AddTo(JSObject? map)
    {
        if (MarkerRef is null || map is null) throw new NullReferenceException();
        LayerInterop.AddTo(MarkerRef, map);
        return this;
    }
    
    public JSObject CreateMarker(LatLng latLng, MarkerOptions options)
    {
        var latLngJson = LeafletInterop.ObjectToJson(latLng);
        var optionsJson = LeafletInterop.ObjectToJson(options);
        var marker = MarkerInterop.CreateMarker(LeafletInterop.JsonToJsObject(latLngJson), LeafletInterop.JsonToJsObject(optionsJson));
        if (Icon is null) return marker;
        var iconOptions = Icon.IconOptions;
        var icon = iconOptions is not null ? Icon.CreateIcon(iconOptions) : Icon.CreateDefaultIcon();
        MarkerInterop.SetIcon(marker, icon);
        return marker;
    }
    
    public void SetLatLng(LatLng latLng)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var latLngJson = LeafletInterop.ObjectToJson(latLng);
        MarkerInterop.SetLatLng(MarkerRef, LeafletInterop.JsonToJsObject(latLngJson));
    }
    
    public void SetOpacity(double opacity)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        MarkerInterop.SetOpacity(MarkerRef, opacity);
    }
    
    public void SetZIndexOffset(double zIndexOffset)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        MarkerInterop.SetZIndexOffset(MarkerRef, zIndexOffset);
    }
    
    public string ToGeoJson(double? precision)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        return MarkerInterop.ToGeoJson(MarkerRef, precision);
    }
    
    public JSObject? GetPopup()
    {
        if (MarkerRef is null) throw new NullReferenceException();
        return MarkerInterop.GetPopup(MarkerRef);
    }
    
    public JSObject? OpenPopup()
    {
        if (MarkerRef is null) throw new NullReferenceException();
        return MarkerInterop.OpenPopup(MarkerRef);
    }
    
    public LatLng? GetLatLng()
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var latLng = MarkerInterop.GetLatLng(MarkerRef);
        return LeafletInterop.JsonToObject<LatLng>(latLng);
    }

    public static partial class MarkerInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static MarkerInterop() { }
        
        [JSImport("createMarker", "BlazorLeafletInterop")]
        public static partial JSObject CreateMarker(JSObject latLng, JSObject options);
        
        [JSImport("getLatLng", "BlazorLeafletInterop")]
        public static partial string GetLatLng(JSObject marker);
        
        [JSImport("setLatLng", "BlazorLeafletInterop")]
        public static partial void SetLatLng(JSObject marker, JSObject latLng);
        
        [JSImport("setIcon", "BlazorLeafletInterop")]
        public static partial void SetIcon(JSObject marker, JSObject icon);
        
        [JSImport("setOpacity", "BlazorLeafletInterop")]
        public static partial void SetOpacity(JSObject marker, double opacity);
        
        [JSImport("setZIndexOffset", "BlazorLeafletInterop")]
        public static partial void SetZIndexOffset(JSObject marker, double zIndexOffset);
        
        [JSImport("toGeoJSON", "BlazorLeafletInterop")]
        public static partial string ToGeoJson(JSObject marker, double? precision);
        
        [JSImport("getPopup", "BlazorLeafletInterop")]
        public static partial JSObject GetPopup(JSObject marker);
        
        [JSImport("openPopup", "BlazorLeafletInterop")]
        public static partial JSObject OpenPopup(JSObject marker);
    }
}