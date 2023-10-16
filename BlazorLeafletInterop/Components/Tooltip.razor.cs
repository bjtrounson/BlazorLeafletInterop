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
    private readonly string _id = Guid.NewGuid().ToString();
    [Parameter] public TooltipOptions TooltipOptions { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [CascadingParameter( Name = "MarkerRef")] public JSObject? MarkerRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        if (!OperatingSystem.IsBrowser()) throw new PlatformNotSupportedException();
        await base.OnInitializedAsync();
        await JSHost.ImportAsync("BlazorLeafletInterop/Tooltip", "../_content/BlazorLeafletInterop/bundle.js");
        if (MarkerRef is null) return;
        var tooltipOptionsJson = LeafletInterop.ObjectToJson(TooltipOptions);
        var tooltipOptions = LeafletInterop.JsonToJsObject(tooltipOptionsJson);
        var tooltipContent = LeafletInterop.GetElementInnerHtml(_id);
        TooltipInterop.BindPopup(MarkerRef, tooltipContent, tooltipOptions);
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
    }
}