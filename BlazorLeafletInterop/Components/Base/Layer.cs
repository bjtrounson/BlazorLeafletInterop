using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Models;

namespace BlazorLeafletInterop.Components.Base;

[SupportedOSPlatform("browser")]
public partial class Layer : Evented
{
    public LayerOptions LayerOptions { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        if (!OperatingSystem.IsBrowser()) throw new PlatformNotSupportedException();
        await base.OnInitializedAsync();
        await JSHost.ImportAsync("BlazorLeafletInterop/Layer", "../_content/BlazorLeafletInterop/bundle.js");
    }

    protected static partial class LayerInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static LayerInterop() {}
        
        [JSImport("addTo", "BlazorLeafletInterop/Layer")]
        public static partial JSObject AddTo(JSObject layer, JSObject map);
        
        [JSImport("remove", "BlazorLeafletInterop/Layer")]
        public static partial JSObject Remove(JSObject layer);
        
        [JSImport("removeFrom", "BlazorLeafletInterop/Layer")]
        public static partial JSObject RemoveFrom(JSObject layer, JSObject map);
        
        [JSImport("getPane", "BlazorLeafletInterop/Layer")]
        public static partial JSObject GetPane(JSObject layer, string? name);
        
        [JSImport("getAttribution", "BlazorLeafletInterop/Layer")]
        public static partial string GetAttribution(JSObject layer);
        
        [JSImport("bindPopup", "BlazorLeafletInterop/Layer")]
        public static partial JSObject BindPopup(JSObject layer, string content, JSObject options);
        
        [JSImport("unbindPopup", "BlazorLeafletInterop/Layer")]
        public static partial JSObject UnbindPopup(JSObject layer);
        
        [JSImport("openPopup", "BlazorLeafletInterop/Layer")]
        public static partial JSObject OpenPopup(JSObject layer, JSObject? latLng);
        
        [JSImport("closePopup", "BlazorLeafletInterop/Layer")]
        public static partial JSObject ClosePopup(JSObject layer);
        
        [JSImport("togglePopup", "BlazorLeafletInterop/Layer")]
        public static partial JSObject TogglePopup(JSObject layer);
        
        [JSImport("isPopupOpen", "BlazorLeafletInterop/Layer")]
        public static partial bool IsPopupOpen(JSObject layer);
        
        [JSImport("setPopupContent", "BlazorLeafletInterop/Layer")]
        public static partial JSObject SetPopupContent(JSObject layer, string content);
        
        [JSImport("getPopup", "BlazorLeafletInterop/Layer")]
        public static partial JSObject GetPopup(JSObject layer);
        
        [JSImport("bindTooltip", "BlazorLeafletInterop/Layer")]
        public static partial JSObject BindTooltip(JSObject layer, string content, JSObject options);
        
        [JSImport("unbindTooltip", "BlazorLeafletInterop/Layer")]
        public static partial JSObject UnbindTooltip(JSObject layer);
        
        [JSImport("openTooltip", "BlazorLeafletInterop/Layer")]
        public static partial JSObject OpenTooltip(JSObject layer, JSObject? latLng);
        
        [JSImport("closeTooltip", "BlazorLeafletInterop/Layer")]
        public static partial JSObject CloseTooltip(JSObject layer);
        
        [JSImport("toggleTooltip", "BlazorLeafletInterop/Layer")]
        public static partial JSObject ToggleTooltip(JSObject layer);
        
        [JSImport("isTooltipOpen", "BlazorLeafletInterop/Layer")]
        public static partial bool IsTooltipOpen(JSObject layer);
        
        [JSImport("setTooltipContent", "BlazorLeafletInterop/Layer")]
        public static partial JSObject SetTooltipContent(JSObject layer, string content);
        
        [JSImport("getTooltip", "BlazorLeafletInterop/Layer")]
        public static partial JSObject GetTooltip(JSObject layer);
    }
}