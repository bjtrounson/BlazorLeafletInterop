using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Models;

namespace BlazorLeafletInterop.Components.Base;

public partial class DivOverlay : InteractiveLayer
{
    public DivOverlayOptions DivOverlayOptions { get; set; } = new();

    public static partial class DivOverlayInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static DivOverlayInterop() { }
        
        [JSImport("openOn", "BlazorLeafletInterop")]
        public static partial JSObject OpenOn(JSObject divOverlay, JSObject map);
        
        [JSImport("close", "BlazorLeafletInterop")]
        public static partial JSObject Close(JSObject divOverlay);
        
        [JSImport("toggle", "BlazorLeafletInterop")]
        public static partial JSObject Toggle(JSObject divOverlay, JSObject? layer);
        
        [JSImport("getLatLng", "BlazorLeafletInterop")]
        public static partial JSObject GetLatLng(JSObject divOverlay);
        
        [JSImport("setLatLng", "BlazorLeafletInterop")]
        public static partial JSObject SetLatLng(JSObject divOverlay, JSObject latLng);
        
        [JSImport("getContent", "BlazorLeafletInterop")]
        public static partial string GetContent(JSObject divOverlay);
        
        [JSImport("setContent", "BlazorLeafletInterop")]
        public static partial JSObject SetContent(JSObject divOverlay, string content);
        
        [JSImport("getElement", "BlazorLeafletInterop")]
        public static partial string GetElement(JSObject divOverlay);
        
        [JSImport("update", "BlazorLeafletInterop")]
        public static partial void Update(JSObject divOverlay);
        
        [JSImport("isOpen", "BlazorLeafletInterop")]
        public static partial bool IsOpen(JSObject divOverlay);
        
        [JSImport("bringToFront", "BlazorLeafletInterop")]
        public static partial JSObject BringToFront(JSObject divOverlay);
        
        [JSImport("bringToBack", "BlazorLeafletInterop")]
        public static partial JSObject BringToBack(JSObject divOverlay);
    }
}