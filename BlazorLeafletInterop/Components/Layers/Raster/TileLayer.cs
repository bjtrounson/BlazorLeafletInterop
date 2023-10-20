using BlazorLeafletInterop.Components.Base;
using BlazorLeafletInterop.Components.Layers.Misc;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models;
using BlazorLeafletInterop.Models.Options.Layer.Raster;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Layers.Raster;

public class TileLayer : GridLayer, IAsyncDisposable
{
    [CascadingParameter(Name = "MapRef")]
    public IJSObjectReference? MapRef { get; set; }

    [Parameter] public string UrlTemplate { get; set; } = "";
    [Parameter] public TileLayerOptions TileLayerOptions{ get; set; } = new();

    private IJSObjectReference? TileRef { get; set; } = null;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        TileRef = await CreateTileLayerAsync(UrlTemplate, TileLayerOptions);
        await AddTo<TileLayer>(MapRef, TileRef).ConfigureAwait(false);
    }
    
    /// <summary>
    /// Instantiates a tile layer object given a URL template and optionally an options object.
    /// </summary>
    /// <param name="urlTemplate"></param>
    /// <param name="tileLayerOptions"></param>
    /// <returns></returns>
    private async Task<IJSObjectReference> CreateTileLayerAsync(string urlTemplate, TileLayerOptions tileLayerOptions)
    {
        var module = await BundleInterop.GetModule();
        var tileLayerOptionsJson = LeafletInterop.ObjectToJson(tileLayerOptions);
        var tileLayerOptionsObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", tileLayerOptionsJson);
        return await module.InvokeAsync<IJSObjectReference>("createTileLayer", urlTemplate, tileLayerOptionsObject);
    }

    /// <summary>
    /// Updates the layer's URL template and redraws it (unless noRedraw is set to true).
    /// If the URL does not change, the layer will not be redrawn unless the noRedraw parameter is set to false.
    /// </summary>
    /// <param name="url"></param>
    /// <param name="noRedraw"></param>
    /// <exception cref="NullReferenceException"></exception>
    public async Task SetUrl(string url, bool noRedraw = false)
    {
        if (TileRef is null) throw new NullReferenceException("TileRef is null");
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("setUrl", TileRef, url, noRedraw);
    }

    public async ValueTask DisposeAsync()
    {
        if (TileRef is null) return;
        if (MapRef is not null) await RemoveFrom<TileLayer>(MapRef, TileRef);
        else await Remove<TileLayer>(TileRef);
        if (TileRef != null) await TileRef.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}