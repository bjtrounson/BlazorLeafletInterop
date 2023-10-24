using BlazorLeafletInterop.Models.Basics;
using BlazorLeafletInterop.Models.Options.Layer.Raster;
using BlazorLeafletInterop.Models.Options.Layer.UI;
using BlazorLeafletInterop.Models.Options.Map;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Factories;

public interface ILayerFactory
{
    public Task<IJSObjectReference> CreateMap(string id, MapOptions mapOptions);
    public Task<IJSObjectReference> CreateTileLayer(string urlTemplate, TileLayerOptions tileLayerOptions);
    public Task<IJSObjectReference> CreateMarker(LatLng latLng, MarkerOptions markerOptions);
    public Task<IJSObjectReference> GetModule();

}