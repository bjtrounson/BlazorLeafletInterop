using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Base;

[SupportedOSPlatform("browser")]
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
        await AddTo(MapRef).ConfigureAwait(false);
    }
    
    private async Task<IJSObjectReference> CreateTileLayerAsync(string urlTemplate, TileLayerOptions tileLayerOptions)
    {
        var module = await BundleInterop.GetModule();
        var tileLayerOptionsJson = LeafletInterop.ObjectToJson(tileLayerOptions);
        var tileLayerOptionsObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", tileLayerOptionsJson);
        return await module.InvokeAsync<IJSObjectReference>("createTileLayer", urlTemplate, tileLayerOptionsObject);
    }
    
    public async Task<TileLayer> AddTo(IJSObjectReference? map)
    {
        if (TileRef is null || map is null) throw new NullReferenceException("TileRef or map is null");
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("addTo", TileRef, map);
        return this;
    }

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