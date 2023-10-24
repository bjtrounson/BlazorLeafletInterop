using System.Reflection;
using BlazorLeafletInterop.Components.Base;
using BlazorLeafletInterop.Components.Layers.Misc;
using BlazorLeafletInterop.Factories;
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
        Module = Map?.Module;
        TileRef = await LayerFactory.CreateTileLayer(UrlTemplate, TileLayerOptions);
        if (Map is null || TileRef is null) return;
        await AddTo<TileLayer>(Map.MapRef, TileRef).ConfigureAwait(false);
    }

    public async Task Initialize(ILayerFactory layerFactory, Map map, string urlTemplate, TileLayerOptions options)
    {
        LayerFactory = layerFactory;
        Map = map;
        Module = await layerFactory.GetModule();
        TileRef = await LayerFactory.CreateTileLayer(urlTemplate, options);
        if (Map is null || TileRef is null) return;
        await AddTo<TileLayer>(Map.MapRef, TileRef).ConfigureAwait(false);
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
        Module ??= await LayerFactory.GetModule();
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