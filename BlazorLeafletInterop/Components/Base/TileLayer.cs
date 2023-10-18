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
public partial class TileLayer : GridLayer, IDisposable
{
    [CascadingParameter(Name = "MapRef")]
    public object? MapRef { get; set; }

    [Parameter] public string UrlTemplate { get; set; } = "";
    [Parameter] public TileLayerOptions TileLayerOptions{ get; set; } = new();

    private object? TileRef { get; set; } = null;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        TileRef = Interop.CreateTileLayer(UrlTemplate, TileLayerOptions.ToJsObject());
        AddTo(MapRef);
    }
    
    public TileLayer AddTo(object? map)
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
        public static partial JSObject CreateTileLayer(string urlTemplate, [JSMarshalAs<JSType.Any>] object options);
        
        [JSImport("setUrl", "BlazorLeafletInterop")]
        public static partial void SetUrl([JSMarshalAs<JSType.Any>] object tileLayer, string url, bool noRedraw);
    }

    public void Dispose()
    {
        if (TileRef is null) return;
        if (MapRef is not null) LayerInterop.RemoveFrom(TileRef, MapRef);
        else LayerInterop.Remove(TileRef);
        TileRef = null;
        GC.SuppressFinalize(this);
    }
}