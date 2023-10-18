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
public partial class LayerGroup : IDisposable
{
    [Parameter] public LayerGroupOptions LayerGroupOptions { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    [CascadingParameter(Name = "MapRef")] public object? MapRef { get; set; }
    
    public object? LayerGroupRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        if (!OperatingSystem.IsBrowser()) throw new PlatformNotSupportedException();
        await base.OnInitializedAsync();
        LayerGroupRef = CreateLayerGroup(LayerGroupOptions);
        if (MapRef is null || LayerGroupRef is null) return;
        LayerInterop.AddTo(LayerGroupRef, MapRef);
    }
    
    public object CreateLayerGroup(LayerGroupOptions options)
    {
        var layerGroupOptionsJson = LeafletInterop.ObjectToJson(options);
        var layerGroupOptions = LeafletInterop.JsonToJsObject(layerGroupOptionsJson);
        return LayerGroupInterop.CreateLayerGroup(layerGroupOptions);
    }
    
    public LayerGroup AddLayer(object layer)
    {
        if (LayerGroupRef is null) throw new NullReferenceException();
        LayerGroupInterop.AddLayer(LayerGroupRef, layer);
        return this;
    }

    public LayerGroup RemoveLayer(object layer)
    {
        if (LayerGroupRef is null) throw new NullReferenceException();
        LayerGroupInterop.RemoveLayer(LayerGroupRef, layer);
        return this;
    }

    public static partial class LayerGroupInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static LayerGroupInterop() { }
        
        [JSImport("createLayerGroup", "BlazorLeafletInterop")]
        public static partial JSObject CreateLayerGroup([JSMarshalAs<JSType.Any>] object options);
        
        [JSImport("addLayer", "BlazorLeafletInterop")]
        public static partial JSObject AddLayer([JSMarshalAs<JSType.Any>] object layerGroup, [JSMarshalAs<JSType.Any>] object layer);
        
        [JSImport("removeLayer", "BlazorLeafletInterop")]
        public static partial JSObject RemoveLayer([JSMarshalAs<JSType.Any>] object layerGroup, [JSMarshalAs<JSType.Any>] object layer);
    }

    public virtual void Dispose()
    {
        if (LayerGroupRef is null || MapRef is null) return;
        LayerInterop.RemoveFrom(LayerGroupRef, MapRef);
        GC.SuppressFinalize(this);
    }
}