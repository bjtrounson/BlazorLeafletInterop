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

    protected static partial class LayerInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static LayerInterop() {}
        
        [JSImport("addTo", "BlazorLeafletInterop")]
        public static partial JSObject AddTo([JSMarshalAs<JSType.Any>] object layer, [JSMarshalAs<JSType.Any>] object map);
        
        [JSImport("remove", "BlazorLeafletInterop")]
        public static partial JSObject Remove([JSMarshalAs<JSType.Any>] object layer);
        
        [JSImport("removeFrom", "BlazorLeafletInterop")]
        public static partial JSObject RemoveFrom([JSMarshalAs<JSType.Any>] object layer, [JSMarshalAs<JSType.Any>] object map);
        
        [JSImport("getPane", "BlazorLeafletInterop")]
        public static partial JSObject GetPane([JSMarshalAs<JSType.Any>] object layer, string? name);
        
        [JSImport("getAttribution", "BlazorLeafletInterop")]
        public static partial JSObject GetAttribution([JSMarshalAs<JSType.Any>] object layer);
        
        [JSImport("bindPopup", "BlazorLeafletInterop")]
        public static partial JSObject BindPopup([JSMarshalAs<JSType.Any>] object layer, string content, [JSMarshalAs<JSType.Any>] object options);
        
        [JSImport("unbindPopup", "BlazorLeafletInterop")]
        public static partial JSObject UnbindPopup([JSMarshalAs<JSType.Any>] object layer);
        
        [JSImport("openPopup", "BlazorLeafletInterop")]
        public static partial JSObject OpenPopup([JSMarshalAs<JSType.Any>] object layer, [JSMarshalAs<JSType.Any>] object? latLng);
        
        [JSImport("closePopup", "BlazorLeafletInterop")]
        public static partial JSObject ClosePopup([JSMarshalAs<JSType.Any>] object layer);
        
        [JSImport("togglePopup", "BlazorLeafletInterop")]
        public static partial JSObject TogglePopup([JSMarshalAs<JSType.Any>] object layer);
        
        [JSImport("isPopupOpen", "BlazorLeafletInterop")]
        public static partial bool IsPopupOpen([JSMarshalAs<JSType.Any>] object layer);
        
        [JSImport("setPopupContent", "BlazorLeafletInterop")]
        public static partial JSObject SetPopupContent([JSMarshalAs<JSType.Any>] object layer, string content);
        
        [JSImport("getPopup", "BlazorLeafletInterop")]
        public static partial JSObject GetPopup([JSMarshalAs<JSType.Any>] object layer);
        
        [JSImport("bindTooltip", "BlazorLeafletInterop")]
        public static partial JSObject BindTooltip([JSMarshalAs<JSType.Any>] object layer, string content, [JSMarshalAs<JSType.Any>] object options);
        
        [JSImport("unbindTooltip", "BlazorLeafletInterop")]
        public static partial JSObject UnbindTooltip([JSMarshalAs<JSType.Any>] object layer);
        
        [JSImport("openTooltip", "BlazorLeafletInterop")]
        public static partial JSObject OpenTooltip([JSMarshalAs<JSType.Any>] object layer, [JSMarshalAs<JSType.Any>] object? latLng);
        
        [JSImport("closeTooltip", "BlazorLeafletInterop")]
        public static partial JSObject CloseTooltip([JSMarshalAs<JSType.Any>] object layer);
        
        [JSImport("toggleTooltip", "BlazorLeafletInterop")]
        public static partial JSObject ToggleTooltip([JSMarshalAs<JSType.Any>] object layer);
        
        [JSImport("isTooltipOpen", "BlazorLeafletInterop")]
        public static partial bool IsTooltipOpen([JSMarshalAs<JSType.Any>] object layer);
        
        [JSImport("setTooltipContent", "BlazorLeafletInterop")]
        public static partial JSObject SetTooltipContent([JSMarshalAs<JSType.Any>] object layer, string content);
        
        [JSImport("getTooltip", "BlazorLeafletInterop")]
        public static partial JSObject GetTooltip([JSMarshalAs<JSType.Any>] object layer);
    }
}