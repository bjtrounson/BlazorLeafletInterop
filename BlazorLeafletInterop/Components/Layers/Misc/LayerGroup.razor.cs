using BlazorLeafletInterop.Models.Options.Layer.Misc;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Layers.Misc;

public partial class LayerGroup : IAsyncDisposable
{
    [Parameter] public LayerGroupOptions LayerGroupOptions { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    [CascadingParameter(Name = "Map")] public Map? Map { get; set; }
    
    public IJSObjectReference? LayerGroupRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        LayerGroupRef = await CreateLayerGroup(LayerGroupOptions);
        if (Map is null || LayerGroupRef is null) return;
        await AddTo<LayerGroup>(Map.MapRef, LayerGroupRef);
    }
    
    public async Task<IJSObjectReference> CreateLayerGroup(LayerGroupOptions options)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("createLayerGroup", options);
    }
    
    public async Task<LayerGroup> AddLayer(IJSObjectReference layer)
    {
        if (LayerGroupRef is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("addLayer", LayerGroupRef, layer);
        return this;
    }

    public async Task<LayerGroup> RemoveLayer(IJSObjectReference layer)
    {
        if (LayerGroupRef is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("removeLayer", LayerGroupRef, layer);
        return this;
    }

    public async ValueTask DisposeAsync()
    {
        if (LayerGroupRef is null || Map is null) return;
        await RemoveFrom<LayerGroup>(Map.MapRef, LayerGroupRef);
        if (LayerGroupRef != null) await LayerGroupRef.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}