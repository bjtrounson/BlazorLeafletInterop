using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorLeafletInterop.Components;

public partial class Tooltip
{
    [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();
    [Parameter] public TooltipOptions TooltipOptions { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [CascadingParameter( Name = "MarkerRef")] public JSObject? MarkerRef { get; set; }
    
    public JSObject? TooltipRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        if (!OperatingSystem.IsBrowser()) throw new PlatformNotSupportedException();
        await base.OnInitializedAsync();
        await JSHost.ImportAsync("BlazorLeafletInterop/Tooltip", "../_content/BlazorLeafletInterop/bundle.js");
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
        
        [JSImport("openOn", "BlazorLeafletInterop/Tooltip")]
        public static partial JSObject OpenOn(JSObject popup, JSObject map);
        
        [JSImport("bindTooltip", "BlazorLeafletInterop/Tooltip")]
        public static partial JSObject BindPopup(JSObject marker, string content, JSObject options);
        
        [JSImport("getTooltip", "BlazorLeafletInterop/Tooltip")]
        public static partial JSObject GetTooltip(JSObject marker);
    }
}