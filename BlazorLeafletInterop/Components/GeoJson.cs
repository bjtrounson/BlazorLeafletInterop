using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models;
using GeoJSON.Net.Feature;
using Microsoft.AspNetCore.Components;

namespace BlazorLeafletInterop.Components;

[SupportedOSPlatform("browser")]
public partial class GeoJson : FeatureGroup
{
    [Parameter] public FeatureCollection GeoJsonData { get; set; } = null!;
    [Parameter] public GeoJsonOptions GeoJsonOptions { get; set; } = new();

    private JSObject? GeoJsonRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await JSHost.ImportAsync("BlazorLeafletInterop/GeoJson", "../_content/BlazorLeafletInterop/bundle.js");
        var geoJsonData = LeafletInterop.ObjectToJson(GeoJsonData);
        GeoJsonRef = GeoJsonInterop.CreateGeoJson(
            LeafletInterop.JsonToObject(geoJsonData),
            GeoJsonOptions.PointToLayer, 
            GeoJsonOptions.Style, 
            GeoJsonOptions.OnEachFeature, 
            GeoJsonOptions.Filter, 
            GeoJsonOptions.MarkersInheritOptions
        );
        AddTo(MapRef);
    }
    
    public GeoJson AddTo(JSObject? map)
    {
        if (GeoJsonRef is null || map is null) throw new NullReferenceException();
        LayerInterop.AddTo(GeoJsonRef, map);
        return this;
    }

    [SupportedOSPlatform("browser")]
    public static partial class GeoJsonInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static GeoJsonInterop() {}
        
        [JSImport("createGeoJson", "BlazorLeafletInterop/GeoJson")]
        public static partial JSObject CreateGeoJson(
            JSObject geoJsonData, 
            [JSMarshalAs<JSType.Function<JSType.Object, JSType.Object>>] Action<JSObject, JSObject>? pointToLayer,
            [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject>? style,
            [JSMarshalAs<JSType.Function<JSType.Object, JSType.Object>>] Action<JSObject, JSObject>? onEachFeature,
            [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject>? filter,
            bool markersInheritOptions
        );
    }
}
    
    
    
    