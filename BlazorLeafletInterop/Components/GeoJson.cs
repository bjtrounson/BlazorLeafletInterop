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
    [Parameter] public FeatureCollection GeoJsonData { get; set; } = new();
    [Parameter] public GeoJsonOptions GeoJsonOptions { get; set; } = new();

    private object? GeoJsonRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var geoJsonData = LeafletInterop.ObjectToJson(GeoJsonData);
        GeoJsonRef = GeoJsonInterop.CreateGeoJson(
            LeafletInterop.JsonToJsObject(geoJsonData),
            GeoJsonOptions.MarkersInheritOptions,
            GeoJsonOptions.PointToLayer is null,
            GeoJsonOptions.Style is null,
            GeoJsonOptions.OnEachFeature is null,
            GeoJsonOptions.Filter is null,
            GeoJsonOptions.PointToLayer,
            GeoJsonOptions.Style,
            GeoJsonOptions.OnEachFeature,
            GeoJsonOptions.Filter
        );
        AddTo(MapRef);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender) return;
        var geoJsonData = LeafletInterop.ObjectToJson(GeoJsonData);
        if (GeoJsonRef is null) return;
        GeoJsonInterop.AddData(GeoJsonRef, LeafletInterop.JsonToJsObject(geoJsonData));
    }

    public GeoJson AddTo(object? map)
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
        
        /// <summary>
        /// Creates GeoJson Layer for the map.
        /// WARNING: The disable flags make sure that JS doesn't use the function pass from C# as it will always pass a function even if null
        /// </summary>
        /// <param name="geoJsonData"></param>
        /// <param name="markersInheritOptions"></param>
        /// <param name="disablePointToLayer"></param>
        /// <param name="disableStyle"></param>
        /// <param name="disableOnEachFeature"></param>
        /// <param name="disableFilter"></param>
        /// <param name="pointToLayer"></param>
        /// <param name="style"></param>
        /// <param name="onEachFeature"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [JSImport("createGeoJson", "BlazorLeafletInterop")]
        public static partial JSObject CreateGeoJson(
            [JSMarshalAs<JSType.Any>] object geoJsonData,
            bool markersInheritOptions,
            bool disablePointToLayer,
            bool disableStyle,
            bool disableOnEachFeature,
            bool disableFilter,
            [JSMarshalAs<JSType.Function<JSType.Any, JSType.Any, JSType.Any>>] Func<object, object, object>? pointToLayer,
            [JSMarshalAs<JSType.Function<JSType.Any>>] Action<object>? style,
            [JSMarshalAs<JSType.Function<JSType.Any, JSType.Any>>] Action<object, object>? onEachFeature,
            [JSMarshalAs<JSType.Function<JSType.Any>>] Action<object>? filter
        );
        
        [JSImport("addData", "BlazorLeafletInterop")]
        public static partial void AddData([JSMarshalAs<JSType.Any>] object geoJson, [JSMarshalAs<JSType.Any>] object geoJsonData);
    }
    
    public override void Dispose()
    {
        if (GeoJsonRef is null || MapRef is null) return;
        LayerInterop.RemoveFrom(GeoJsonRef, MapRef);
        GC.SuppressFinalize(this);
    }
}
    
    
    
    