using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorLeafletInterop.Components.Base;

[SupportedOSPlatform("browser")]
public partial class GridLayer : Layer
{
    public GridLayerOptions GridLayerOptions { get; set; } = new();

    public static partial class GridLayerInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static GridLayerInterop() {}
        
        [JSImport("bringToFront", "BlazorLeafletInterop")]
        public static partial JSObject BringToFront(JSObject gridLayer);
        
        [JSImport("bringToBack", "BlazorLeafletInterop")]
        public static partial JSObject BringToBack(JSObject gridLayer);
        
        [JSImport("getContainer", "BlazorLeafletInterop")]
        public static partial JSObject GetContainer(JSObject gridLayer);
        
        [JSImport("setOpacity", "BlazorLeafletInterop")]
        public static partial JSObject SetOpacity(JSObject gridLayer, double opacity);
        
        [JSImport("setZIndex", "BlazorLeafletInterop")]
        public static partial JSObject SetZIndex(JSObject gridLayer, int zIndex);
        
        [JSImport("isLoading", "BlazorLeafletInterop")]
        public static partial bool IsLoading(JSObject gridLayer);
        
        [JSImport("redraw", "BlazorLeafletInterop")]
        public static partial JSObject Redraw(JSObject gridLayer);
        
        [JSImport("getTileSize", "BlazorLeafletInterop")]
        public static partial JSObject GetTileSize(JSObject gridLayer);
    }
}