using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models.Basics;
using BlazorLeafletInterop.Models.Options.Layer.Raster;
using BlazorLeafletInterop.Models.Options.Layer.UI;
using BlazorLeafletInterop.Models.Options.Map;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Factories;

public class LayerFactory : ILayerFactory
{
    private const string CreateMapFunction = "createMap";
    private const string CreateMarkerFunction = "createMarker";
    private const string CreateTileLayerFunction = "createTileLayer";
    
    private readonly IBundleInterop _bundleInterop;
    private IJSObjectReference? _module;
    
    public LayerFactory(IBundleInterop bundleInterop)
    {
        _bundleInterop = bundleInterop;
    }
    
    public async Task<IJSObjectReference> CreateMap(string id, MapOptions mapOptions)
    {
        _module ??= await _bundleInterop.GetModule();
        var mapOptionsJson = LeafletInterop.ObjectToJson(mapOptions);
        var mapOptionsObject = await _module.InvokeAsync<IJSObjectReference>("jsonToJsObject", mapOptionsJson);
        return await _module.InvokeAsync<IJSObjectReference>(CreateMapFunction, id, mapOptionsObject);
    }
    
    public async Task<IJSObjectReference> CreateMarker(LatLng latLng, MarkerOptions markerOptions)
    {
        _module ??= await _bundleInterop.GetModule();
        var markerOptionsJson = LeafletInterop.ObjectToJson(markerOptions);
        var markerOptionsObject = await _module.InvokeAsync<IJSObjectReference>("jsonToJsObject", markerOptionsJson);
        return await _module.InvokeAsync<IJSObjectReference>(CreateMarkerFunction, latLng, markerOptionsObject);
    }

    public async Task<IJSObjectReference> CreateTileLayer(string urlTemplate, TileLayerOptions tileLayerOptions)
    {
        _module ??= await _bundleInterop.GetModule();
        var tileLayerOptionsJson = LeafletInterop.ObjectToJson(tileLayerOptions);
        var tileLayerOptionsObject = await _module.InvokeAsync<IJSObjectReference>("jsonToJsObject", tileLayerOptionsJson);
        return await _module.InvokeAsync<IJSObjectReference>(CreateTileLayerFunction, urlTemplate, tileLayerOptionsObject);
    }

    public async Task<IJSObjectReference> GetModule()
    {
        return _module ?? await _bundleInterop.GetModule();
    }
}