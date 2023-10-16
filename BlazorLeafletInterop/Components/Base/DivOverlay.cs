using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Models;

namespace BlazorLeafletInterop.Components.Base;

public partial class DivOverlay : InteractiveLayer
{
    public DivOverlayOptions DivOverlayOptions { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        if (!OperatingSystem.IsBrowser()) throw new PlatformNotSupportedException();
        await base.OnInitializedAsync();
        await JSHost.ImportAsync("BlazorLeafletInterop/DivOverlay", "../_content/BlazorLeafletInterop/bundle.js");
    }

    public static partial class DivOverlayInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static DivOverlayInterop() { }
        
        [JSImport("openOn", "BlazorLeafletInterop/DivOverlay")]
        public static partial JSObject OpenOn(JSObject divOverlay, JSObject map);
        
        [JSImport("close", "BlazorLeafletInterop/DivOverlay")]
        public static partial JSObject Close(JSObject divOverlay);
        
        [JSImport("toggle", "BlazorLeafletInterop/DivOverlay")]
        public static partial JSObject Toggle(JSObject divOverlay, JSObject? layer);
        
        [JSImport("getOverlayLatLng", "BlazorLeafletInterop/DivOverlay")]
        public static partial JSObject GetLatLng(JSObject divOverlay);
        
        [JSImport("setOverlayLatLng", "BlazorLeafletInterop/DivOverlay")]
        public static partial JSObject SetLatLng(JSObject divOverlay, JSObject latLng);
        
        [JSImport("getContent", "BlazorLeafletInterop/DivOverlay")]
        public static partial string GetContent(JSObject divOverlay);
        
        [JSImport("setContent", "BlazorLeafletInterop/DivOverlay")]
        public static partial JSObject SetContent(JSObject divOverlay, string content);
        
        [JSImport("getElement", "BlazorLeafletInterop/DivOverlay")]
        public static partial string GetElement(JSObject divOverlay);
        
        [JSImport("update", "BlazorLeafletInterop/DivOverlay")]
        public static partial void Update(JSObject divOverlay);
        
        [JSImport("isOpen", "BlazorLeafletInterop/DivOverlay")]
        public static partial bool IsOpen(JSObject divOverlay);
        
        [JSImport("bringOverlayToFront", "BlazorLeafletInterop/DivOverlay")]
        public static partial JSObject BringToFront(JSObject divOverlay);
        
        [JSImport("bringOverlayToBack", "BlazorLeafletInterop/DivOverlay")]
        public static partial JSObject BringToBack(JSObject divOverlay);
    }
}