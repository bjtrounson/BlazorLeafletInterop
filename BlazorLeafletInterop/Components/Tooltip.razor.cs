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
public partial class Tooltip : IDisposable
{
    [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();
    [Parameter] public TooltipOptions TooltipOptions { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [CascadingParameter( Name = "MarkerRef")] public object? MarkerRef { get; set; }
    
    public object? TooltipRef { get; set; }
    
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (!firstRender) return;
        if (MarkerRef is null) return;
        var tooltipOptionsJson = LeafletInterop.ObjectToJson(TooltipOptions);
        var tooltipOptions = LeafletInterop.JsonToJsObject(tooltipOptionsJson);
        var tooltipContent = LeafletInterop.GetElementInnerHtml(Id);
        TooltipInterop.BindPopup(MarkerRef, tooltipContent, tooltipOptions);
        TooltipRef = TooltipInterop.GetTooltip(MarkerRef);
    }

    public static partial class TooltipInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static TooltipInterop() { }
        
        [JSImport("openOn", "BlazorLeafletInterop")]
        public static partial JSObject OpenOn([JSMarshalAs<JSType.Any>] object popup, [JSMarshalAs<JSType.Any>] object map);
        
        [JSImport("bindTooltip", "BlazorLeafletInterop")]
        public static partial JSObject BindPopup([JSMarshalAs<JSType.Any>] object marker, string content, [JSMarshalAs<JSType.Any>] object options);
        
        [JSImport("unbindTooltip", "BlazorLeafletInterop")]
        public static partial JSObject UnbindPopup([JSMarshalAs<JSType.Any>] object marker);
        
        [JSImport("getTooltip", "BlazorLeafletInterop")]
        public static partial JSObject GetTooltip([JSMarshalAs<JSType.Any>] object marker);
    }

    public void Dispose()
    {
        if (MarkerRef is null) return;
        TooltipInterop.UnbindPopup(MarkerRef);
        GC.SuppressFinalize(this);
    }
}