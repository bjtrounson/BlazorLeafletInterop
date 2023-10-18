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
public partial class Popup : IDisposable
{
    [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();
    [Parameter] public PopupOptions PopupOptions { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [CascadingParameter( Name = "MarkerRef")] public object? MarkerRef { get; set; }
    
    public object? PopupRef { get; set; }

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
        public static partial JSObject OpenOn([JSMarshalAs<JSType.Any>] object popup, [JSMarshalAs<JSType.Any>] object map);
        
        [JSImport("bindPopup", "BlazorLeafletInterop")]
        public static partial JSObject BindPopup([JSMarshalAs<JSType.Any>] object marker, string content, [JSMarshalAs<JSType.Any>] object options);
        
        [JSImport("unbindPopup", "BlazorLeafletInterop")]
        public static partial JSObject UnbindPopup([JSMarshalAs<JSType.Any>] object marker);
        
        [JSImport("getPopup", "BlazorLeafletInterop")]
        public static partial JSObject GetPopup([JSMarshalAs<JSType.Any>] object marker);
    }

    public void Dispose()
    {
        if (MarkerRef is null) return;
        PopupInterop.UnbindPopup(MarkerRef);
        GC.SuppressFinalize(this);
    }
}