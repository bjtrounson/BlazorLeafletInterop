using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Components;

namespace BlazorLeafletInterop.Components.Base;

[SupportedOSPlatform("browser")]
public partial class TileLayer : GridLayer
{
    [CascadingParameter]
    public JSObject? MapRef { get; set; }

    [Parameter] public string UrlTemplate { get; set; } = "";

    private JSObject? TileRef { get; set; } = null;

    protected override async Task OnInitializedAsync()
    {
        await JSHost.ImportAsync("BlazorLeafletInterop/TileLayer", "../_content/BlazorLeafletInterop/bundle.js");
        TileRef = Interop.CreateTileLayer(UrlTemplate, null);
        AddTo(MapRef);
    }
    
    public TileLayer AddTo(JSObject? map)
    {
        if (TileRef is null || map is null) throw new NullReferenceException();
        Interop.AddTo(TileRef, map);
        return this;
    }
    
    [SupportedOSPlatform("browser")]
    public partial class Interop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static Interop() {}

        [JSImport("createTileLayer", "BlazorLeafletInterop/TileLayer")]
        public static partial JSObject CreateTileLayer(string urlTemplate, [JSMarshalAs<JSType.Any>] object options);
        
        [JSImport("addTileLayer", "BlazorLeafletInterop/TileLayer")]
        public static partial void AddTo([JSMarshalAs<JSType.Any>] object tileLayer, [JSMarshalAs<JSType.Any>] object layer);
    }
}