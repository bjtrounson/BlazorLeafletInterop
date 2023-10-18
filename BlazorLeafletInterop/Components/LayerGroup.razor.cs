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
public partial class LayerGroup
{
    [Parameter] public LayerGroupOptions LayerGroupOptions { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    [CascadingParameter(Name = "MapRef")] public JSObject? MapRef { get; set; }
    
    public JSObject? LayerGroupRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        if (!OperatingSystem.IsBrowser()) throw new PlatformNotSupportedException();
        await base.OnInitializedAsync();
        LayerGroupRef = CreateLayerGroup(LayerGroupOptions);
        if (MapRef is null || LayerGroupRef is null) return;
        LayerInterop.AddTo(LayerGroupRef, MapRef);
    }
    
    public JSObject CreateLayerGroup(LayerGroupOptions options)
    {
        var layerGroupOptionsJson = LeafletInterop.ObjectToJson(options);
        var layerGroupOptions = LeafletInterop.JsonToJsObject(layerGroupOptionsJson);
        return LayerGroupInterop.CreateLayerGroup(layerGroupOptions);
    }
    
    public LayerGroup AddLayer(JSObject layer)
    {
        if (LayerGroupRef is null) throw new NullReferenceException();
        LayerGroupInterop.AddLayer(LayerGroupRef, layer);
        return this;
    }

    public static partial class LayerGroupInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static LayerGroupInterop() { }
        
        [JSImport("createLayerGroup", "BlazorLeafletInterop")]
        public static partial JSObject CreateLayerGroup(JSObject options);
        
        [JSImport("addLayer", "BlazorLeafletInterop")]
        public static partial JSObject AddLayer(JSObject layerGroup, JSObject layer);
    }
}