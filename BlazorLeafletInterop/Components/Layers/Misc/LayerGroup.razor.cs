using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models;
using BlazorLeafletInterop.Models.Options.Layer.Misc;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Layers.Misc;

public partial class LayerGroup : IAsyncDisposable
{
    [Parameter] public LayerGroupOptions LayerGroupOptions { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    [CascadingParameter(Name = "MapRef")] public IJSObjectReference? MapRef { get; set; }
    
    public IJSObjectReference? LayerGroupRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        LayerGroupRef = await CreateLayerGroup(LayerGroupOptions);
        if (MapRef is null || LayerGroupRef is null) return;
        await AddTo<LayerGroup>(MapRef, LayerGroupRef);
    }
    
    public async Task<IJSObjectReference> CreateLayerGroup(LayerGroupOptions options)
    {
        var layerGroupOptionsJson = LeafletInterop.ObjectToJson(options);
        var module = await BundleInterop.GetModule();
        var layerGroupOptions = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", layerGroupOptionsJson);
        return await module.InvokeAsync<IJSObjectReference>("createLayerGroup", layerGroupOptions);
    }
    
    public async Task<LayerGroup> AddLayer(IJSObjectReference layer)
    {
        if (LayerGroupRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("addLayer", LayerGroupRef, layer);
        return this;
    }

    public async Task<LayerGroup> RemoveLayer(IJSObjectReference layer)
    {
        if (LayerGroupRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("removeLayer", LayerGroupRef, layer);
        return this;
    }

    public async ValueTask DisposeAsync()
    {
        if (LayerGroupRef is null || MapRef is null) return;
        await RemoveFrom<LayerGroup>(LayerGroupRef, MapRef);
        if (LayerGroupRef != null) await LayerGroupRef.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}