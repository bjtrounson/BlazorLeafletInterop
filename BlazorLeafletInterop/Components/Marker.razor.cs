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
        await JSHost.ImportAsync("BlazorLeafletInterop/Marker", "../_content/BlazorLeafletInterop/bundle.js");
        MarkerRef = await CreateMarker(LatLng, Options);
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
    
    public async Task<JSObject> CreateMarker(LatLng latLng, MarkerOptions options)
    {
        var latLngJson = LeafletInterop.ObjectToJson(latLng);
        var optionsJson = LeafletInterop.ObjectToJson(options);
        var marker = MarkerInterop.CreateMarker(LeafletInterop.JsonToObject(latLngJson), LeafletInterop.JsonToObject(optionsJson));
        if (Icon is null) return marker;
        var iconOptions = Icon.IconOptions;
        var icon = iconOptions is not null ? await Icon.CreateIcon(iconOptions) : await Icon.CreateDefaultIcon();
        MarkerInterop.SetIcon(marker, icon);
        return marker;
    }

    public static partial class MarkerInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static MarkerInterop() { }
        
        [JSImport("createMarker", "BlazorLeafletInterop/Marker")]
        public static partial JSObject CreateMarker(JSObject latLng, JSObject options);
        
        [JSImport("getLatLng", "BlazorLeafletInterop/Marker")]
        public static partial JSObject GetLatLng(JSObject marker);
        
        [JSImport("setLatLng", "BlazorLeafletInterop/Marker")]
        public static partial void SetLatLng(JSObject marker, JSObject latLng);
        
        [JSImport("setIcon", "BlazorLeafletInterop/Marker")]
        public static partial void SetIcon(JSObject marker, JSObject icon);
        
        [JSImport("setOpacity", "BlazorLeafletInterop/Marker")]
        public static partial void SetOpacity(JSObject marker, double opacity);
        
        [JSImport("setZIndexOffset", "BlazorLeafletInterop/Marker")]
        public static partial void SetZIndexOffset(JSObject marker, double zIndexOffset);
        
        [JSImport("toGeoJSON", "BlazorLeafletInterop/Marker")]
        public static partial JSObject ToGeoJson(JSObject marker, double? precision);
        
        [JSImport("log", "BlazorLeafletInterop/Marker")]
        public static partial void Log(string message);
    }
}