using BlazorLeafletInterop.Components.Base;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models.Options.Control;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using IJSObjectReference = Microsoft.JSInterop.IJSObjectReference;

namespace BlazorLeafletInterop.Components.Controls;

public class ControlLayers : Control
{
    [Parameter] public ControlLayersOptions LayersOptions { get; set; } = new();
    [Parameter] public Dictionary<string, IJSObjectReference> BaseLayers { get; set; } = new();
    [Parameter] public Dictionary<string, IJSObjectReference> OverlayLayers { get; set; } = new();
    
    private IJSObjectReference? LayersRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        LayersRef = await CreateLayers(BaseLayers, OverlayLayers, LayersOptions);
        if (Map is null || LayersRef is null) return;
        await AddTo<ControlLayers>(Map.MapRef, LayersRef);
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        if (LayersRef is null) return;
        // Remove the old layers control
        await Remove<ControlLayers>(LayersRef);
        // Create a new layers control
        LayersRef = await CreateLayers(BaseLayers, OverlayLayers, LayersOptions);
        if (Map is null || LayersRef is null) return;
        await AddTo<ControlLayers>(Map.MapRef, LayersRef);
    }

    private async Task<IJSObjectReference> CreateLayers(
        Dictionary<string, IJSObjectReference> baseLayers, 
        Dictionary<string, IJSObjectReference> overlayLayers, 
        ControlLayersOptions? layersOptions)
    {
        Module ??= await LayerFactory.GetModule();
        // convert the baselayers and overlayers to js objects
        var baseLayersOptions = LeafletInterop.ObjectToJson(baseLayers);
        var overlayLayersOptions = LeafletInterop.ObjectToJson(overlayLayers);
        var baseLayersObject = await Module.InvokeAsync<IJSObjectReference>("jsonToJsObject", baseLayersOptions);
        var overlayLayersObject = await Module.InvokeAsync<IJSObjectReference>("jsonToJsObject", overlayLayersOptions);
        return await Module.InvokeAsync<IJSObjectReference>("createLayers", baseLayersObject, overlayLayersObject, layersOptions);
    }
    
    /// <summary>
    /// Adds a base layer (radio button entry) with the given name to the control.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="name"></param>
    /// <returns>this</returns>
    public async Task<ControlLayers> AddBaseLayer(IJSObjectReference layer, string name)
    {
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("addBaseLayer", LayersRef, layer, name);
        return this;
    }
    
    /// <summary>
    /// Adds an overlay (checkbox entry) with the given name to the control.
    /// </summary>
    /// <param name="layer"></param>
    /// <returns>this</returns>
    public async Task<ControlLayers> RemoveLayer(IJSObjectReference layer)
    {
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("removeLayer", LayersRef, layer);
        return this;
    }
    
    /// <summary>
    /// Adds an overlay (checkbox entry) with the given name to the control.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="name"></param>
    /// <returns>this</returns>
    public async Task<ControlLayers> AddOverlay(IJSObjectReference layer, string name)
    {
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("addOverlay", LayersRef, layer, name);
        return this;
    }
}