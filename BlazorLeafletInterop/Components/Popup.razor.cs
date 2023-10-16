using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorLeafletInterop.Components;

public partial class Popup
{
    private readonly string _id = Guid.NewGuid().ToString();
    [Parameter] public PopupOptions PopupOptions { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [CascadingParameter( Name = "MarkerRef")] public JSObject? MarkerRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        if (!OperatingSystem.IsBrowser()) throw new PlatformNotSupportedException();
        await base.OnInitializedAsync();
        await JSHost.ImportAsync("BlazorLeafletInterop/Popup", "../_content/BlazorLeafletInterop/bundle.js");
        if (MarkerRef is null) return;
        var popupOptionsJson = LeafletInterop.ObjectToJson(PopupOptions);
        var popupOptions = LeafletInterop.JsonToObject(popupOptionsJson);
        var popupContent = LeafletInterop.GetElementInnerHtml(_id);
        PopupInterop.BindPopup(MarkerRef, popupContent, popupOptions);
    }

    public static partial class PopupInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static PopupInterop() { }
        
        [JSImport("openOn", "BlazorLeafletInterop/Popup")]
        public static partial JSObject OpenOn(JSObject popup, JSObject map);
        
        [JSImport("bindPopup", "BlazorLeafletInterop/Popup")]
        public static partial JSObject BindPopup(JSObject marker, string content, JSObject options);
    }
}