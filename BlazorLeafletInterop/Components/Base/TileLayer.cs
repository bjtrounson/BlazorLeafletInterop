using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorLeafletInterop.Components.Base;

[SupportedOSPlatform("browser")]
public partial class TileLayer : GridLayer
{
    [CascadingParameter(Name = "MapRef")]
    public JSObject? MapRef { get; set; }

    [Parameter] public string UrlTemplate { get; set; } = "";
    [Parameter] public TileLayerOptions TileLayerOptions{ get; set; } = new();

    private JSObject? TileRef { get; set; } = null;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        TileRef = Interop.CreateTileLayer(UrlTemplate, TileLayerOptions.ToJsObject());
        AddTo(MapRef);
    }
    
    public TileLayer AddTo(JSObject? map)
    {
        if (TileRef is null || map is null) throw new NullReferenceException();
        LayerInterop.AddTo(TileRef, map);
        return this;
    }
    
    [SupportedOSPlatform("browser")]
    public partial class Interop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static Interop() {}

        [JSImport("createTileLayer", "BlazorLeafletInterop")]
        public static partial JSObject CreateTileLayer(string urlTemplate, JSObject options);
        
        [JSImport("setUrl", "BlazorLeafletInterop")]
        public static partial void SetUrl(JSObject tileLayer, string url, bool noRedraw);
    }
}