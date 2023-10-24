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
    [CascadingParameter(Name = "Map")]
    public Map? Map { get; set; }

    [Parameter] public string UrlTemplate { get; set; } = "";
    [Parameter] public TileLayerOptions TileLayerOptions{ get; set; } = new();

    private IJSObjectReference? TileRef { get; set; } = null;

    protected override async Task OnInitializedAsync()
    {
        TileRef = await CreateTileLayerAsync(UrlTemplate, TileLayerOptions);
        if (Map is null || TileRef is null) return;
        await AddTo<TileLayer>(Map.MapRef, TileRef).ConfigureAwait(false);
    }

    public async Task Initialize(IBundleInterop interop, Map map, string urlTemplate, TileLayerOptions options)
    {
        BundleInterop = interop;
        Map = map;
        TileRef = await CreateTileLayerAsync(urlTemplate, options);
        if (Map is null || TileRef is null) return;
        await AddTo<TileLayer>(Map.MapRef, TileRef).ConfigureAwait(false);
    }

    /// <summary>
    /// Instantiates a tile layer object given a URL template and optionally an options object.
    /// </summary>
    /// <param name="urlTemplate"></param>
    /// <param name="tileLayerOptions"></param>
    /// <returns></returns>
    private async Task<IJSObjectReference> CreateTileLayerAsync(string urlTemplate, TileLayerOptions tileLayerOptions)
    {
        Module ??= await BundleInterop.GetModule();
        var tileLayerOptionsJson = LeafletInterop.ObjectToJson(tileLayerOptions);
        var tileLayerOptionsObject = await Module.InvokeAsync<IJSObjectReference>("jsonToJsObject", tileLayerOptionsJson);
        return await Module.InvokeAsync<IJSObjectReference>("createTileLayer", urlTemplate, tileLayerOptionsObject);
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
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("setUrl", TileRef, url, noRedraw);
    }

    public async ValueTask DisposeAsync()
    {
        if (TileRef is null) return;
        if (Map is not null) await RemoveFrom<TileLayer>(Map.MapRef, TileRef);
        else await Remove<TileLayer>(TileRef);
        if (TileRef != null) await TileRef.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}