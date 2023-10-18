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
public partial class Marker : IDisposable
{
    [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();
    [Parameter] public LatLng LatLng { get; set; } = new();
    [Parameter] public Icon? Icon { get; set; }
    [Parameter] public MarkerOptions Options { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    [CascadingParameter(Name = "LayerGroup")] public LayerGroup? LayerGroup { get; set; }
    [CascadingParameter(Name = "MapRef")] public object? MapRef { get; set; }
    
    public object? MarkerRef { get; set; }

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
    
    public Marker AddTo(object? map)
    {
        if (MarkerRef is null || map is null) throw new NullReferenceException();
        LayerInterop.AddTo(MarkerRef, map);
        return this;
    }
    
    public object CreateMarker(LatLng latLng, MarkerOptions options)
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
    
    public object? GetPopup()
    {
        if (MarkerRef is null) throw new NullReferenceException();
        return MarkerInterop.GetPopup(MarkerRef);
    }
    
    public object? OpenPopup()
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
        public static partial JSObject CreateMarker([JSMarshalAs<JSType.Any>] object latLng, [JSMarshalAs<JSType.Any>] object options);
        
        [JSImport("getLatLng", "BlazorLeafletInterop")]
        public static partial string GetLatLng([JSMarshalAs<JSType.Any>] object marker);
        
        [JSImport("setLatLng", "BlazorLeafletInterop")]
        public static partial void SetLatLng([JSMarshalAs<JSType.Any>] object marker, [JSMarshalAs<JSType.Any>] object latLng);
        
        [JSImport("setIcon", "BlazorLeafletInterop")]
        public static partial void SetIcon([JSMarshalAs<JSType.Any>] object marker, [JSMarshalAs<JSType.Any>] object icon);
        
        [JSImport("setOpacity", "BlazorLeafletInterop")]
        public static partial void SetOpacity([JSMarshalAs<JSType.Any>] object marker, double opacity);
        
        [JSImport("setZIndexOffset", "BlazorLeafletInterop")]
        public static partial void SetZIndexOffset([JSMarshalAs<JSType.Any>] object marker, double zIndexOffset);
        
        [JSImport("toGeoJSON", "BlazorLeafletInterop")]
        public static partial string ToGeoJson([JSMarshalAs<JSType.Any>] object marker, double? precision);
        
        [JSImport("getPopup", "BlazorLeafletInterop")]
        public static partial JSObject GetPopup([JSMarshalAs<JSType.Any>] object marker);
        
        [JSImport("openPopup", "BlazorLeafletInterop")]
        public static partial JSObject OpenPopup([JSMarshalAs<JSType.Any>] object marker);
    }

    public void Dispose()
    {
        if (MarkerRef is null) return;
        LayerGroup?.RemoveLayer(MarkerRef);
        if (MapRef is not null) LayerInterop.RemoveFrom(MarkerRef, MapRef);
        GC.SuppressFinalize(this);
    }
}