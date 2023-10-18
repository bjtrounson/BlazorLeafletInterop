using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorLeafletInterop.Components;

[SupportedOSPlatform("browser")]
public partial class Popup
{
    [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();
    [Parameter] public PopupOptions PopupOptions { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [CascadingParameter( Name = "MarkerRef")] public JSObject? MarkerRef { get; set; }
    
    public JSObject? PopupRef { get; set; }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (!firstRender) return;
        if (MarkerRef is null) return;
        var popupOptionsJson = LeafletInterop.ObjectToJson(PopupOptions);
        var popupOptions = LeafletInterop.JsonToJsObject(popupOptionsJson);
        var popupContent = LeafletInterop.GetElementInnerHtml(Id);
        PopupInterop.BindPopup(MarkerRef, popupContent, popupOptions);
        PopupRef = PopupInterop.GetPopup(MarkerRef);
    }

    public static partial class PopupInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static PopupInterop() { }
        
        [JSImport("openOn", "BlazorLeafletInterop")]
        public static partial JSObject OpenOn(JSObject popup, JSObject map);
        
        [JSImport("bindPopup", "BlazorLeafletInterop")]
        public static partial JSObject BindPopup(JSObject marker, string content, JSObject options);
        
        [JSImport("getPopup", "BlazorLeafletInterop")]
        public static partial JSObject GetPopup(JSObject marker);
    }
}