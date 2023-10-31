using BlazorLeafletInterop.Components.Base;
using BlazorLeafletInterop.Components.Layers.Raster;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Plugins.Leaflet.MiniMap.Models.Options;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Plugins.Leaflet.MiniMap.Components;


/// <summary>
/// You need to include the Leaflet.MiniMap.js in your index.html file or _Host.cshtml file to use this plugin
/// </summary>
public class LeafletMiniMap : Control
{
    [Parameter] public LeafletMiniMapOptions MiniMapOptions { get; set; } = new();
    [Parameter] public IJSObjectReference? TileRef { get; set; }
    
    private IJSObjectReference? MiniMapRef { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        // Remove it from the map if it already exists
        if (MiniMapRef is not null && Map is not null) await Map.RemoveControl(MiniMapRef);
        // Create a new minimap control
        if (TileRef is null) return;
        MiniMapRef = await CreateMiniMap(TileRef, MiniMapOptions);
        if (Map is null || MiniMapRef is null) return;
        await AddTo<LeafletMiniMap>(Map.MapRef, MiniMapRef);
    }
    
    private async Task<IJSObjectReference> CreateMiniMap(IJSObjectReference layer, LeafletMiniMapOptions? miniMapOptions)
    {
        Module ??= await LayerFactory.GetModule();
        // convert the minimap options to js objects
        miniMapOptions ??= new LeafletMiniMapOptions();
        var miniMapOptionsJson = LeafletInterop.ObjectToJson(miniMapOptions);
        var miniMapOptionsObject = await Module.InvokeAsync<IJSObjectReference>("jsonToJsObject", miniMapOptionsJson);
        return await Module.InvokeAsync<IJSObjectReference>("createMiniMap", layer, miniMapOptionsObject);
    }
    
    /// <summary>
    /// Change the layer the minimap is showing
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    public async Task<LeafletMiniMap> ChangeLayer(IJSObjectReference layer)
    {
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("changeLayer", MiniMapRef, layer);
        return this;
    }
}