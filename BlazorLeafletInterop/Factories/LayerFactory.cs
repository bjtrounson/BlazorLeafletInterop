using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models.Basics;
using BlazorLeafletInterop.Models.Options.Layer.Misc;
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
    private const string CreateLayerGroupFunction = "createLayerGroup";
    private const string CreateFeatureGroupFunction = "createFeatureGroup";
    
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

    public async Task<IJSObjectReference> CreateLayerGroup(LayerGroupOptions layerGroupOptions)
    {
        _module ??= await _bundleInterop.GetModule();
        var layerGroupOptionsJson = LeafletInterop.ObjectToJson(layerGroupOptions);
        var options = await _module.InvokeAsync<IJSObjectReference>("jsonToJsObject", layerGroupOptionsJson);
        return await _module.InvokeAsync<IJSObjectReference>(CreateLayerGroupFunction, options);
    }

    public async Task<IJSObjectReference> CreateFeatureGroup(LayerGroupOptions featureGroupOptions)
    {
        _module ??= await _bundleInterop.GetModule();
        var featureGroupOptionsJson = LeafletInterop.ObjectToJson(featureGroupOptions);
        var options = await _module.InvokeAsync<IJSObjectReference>("jsonToJsObject", featureGroupOptionsJson);
        return await _module.InvokeAsync<IJSObjectReference>(CreateFeatureGroupFunction, options);
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