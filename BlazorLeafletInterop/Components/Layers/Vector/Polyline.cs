using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models.Basics;
using BlazorLeafletInterop.Models.Options.Layer.Vector;
using GeoJSON.Net.Feature;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Layers.Vector;

public class Polyline : Path
{
    [CascadingParameter] public IJSObjectReference? MapRef { get; set; }
    [Parameter] public PolylineOptions PolylineOptions { get; set; } = new();
    [Parameter] public virtual List<LatLng> LatLngs { get; set; } = new();
    
    public IJSObjectReference? PolylineRef { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if (MapRef is null) return;
        PolylineRef = await CreatePolyline(PolylineOptions);
        if (PolylineRef is null) return;
        await SetLatLngs(LatLngs);
        await AddTo<Polyline>(MapRef, PolylineRef);
    }
    
    public async Task Init(IBundleInterop bundleInterop, IJSObjectReference? mapRef, PolylineOptions? options = null)
    {
        if (mapRef is null) return;
        MapRef = mapRef;
        BundleInterop = bundleInterop;
        PolylineRef = await CreatePolyline(options ?? new PolylineOptions());
    }

    public async Task<IJSObjectReference> CreatePolyline(PolylineOptions options)
    {
        var module = await BundleInterop.GetModule();
        var optionsJson = LeafletInterop.ObjectToJson(options);
        var optionsObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", optionsJson);
        var latLngsJson = LeafletInterop.ObjectToJson(LatLngs);
        var latLngsObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", latLngsJson);
        return await module.InvokeAsync<IJSObjectReference>("createPolyline", latLngsObject, optionsObject);
    }
    
    public async Task<Polyline> SetLatLngs(List<LatLng> latLngs)
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        var module = await BundleInterop.GetModule();
        var latLngsJson = LeafletInterop.ObjectToJson(latLngs);
        var latLngsObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", latLngsJson);
        await module.InvokeAsync<IJSObjectReference>("setLatLngs", PolylineRef, latLngsObject);
        return this;
    }

    public async Task<Polyline> AddLatLng(LatLng latLng)
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        var module = await BundleInterop.GetModule();
        var latLngJson = LeafletInterop.ObjectToJson(latLng);
        var latLngObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", latLngJson);
        await module.InvokeAsync<IJSObjectReference>("addLatLng", PolylineRef, latLngObject);
        return this;
    }
    
    public async Task<LatLng[]?> GetLatLngs()
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        var module = await BundleInterop.GetModule();
        var latLngsJson = await module.InvokeAsync<string>("getLatLngs", PolylineRef);
        return LeafletInterop.JsonToObject<LatLng[]>(latLngsJson);
    }
    
    public async Task<FeatureCollection?> ToGeoJson()
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        var module = await BundleInterop.GetModule();
        var geojson = await module.InvokeAsync<string>("toGeoJSON", PolylineRef);
        return LeafletInterop.JsonToObject<FeatureCollection>(geojson);
    }
    
    public async Task<LatLngBounds?> GetBounds()
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        var module = await BundleInterop.GetModule();
        var boundsJson = await module.InvokeAsync<string>("getBounds", PolylineRef);
        return LeafletInterop.JsonToObject<LatLngBounds>(boundsJson);
    }

    public async Task<LatLng?> GetCenter()
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        var module = await BundleInterop.GetModule();
        var latLngJson = await module.InvokeAsync<string>("getCenter", PolylineRef);
        return LeafletInterop.JsonToObject<LatLng>(latLngJson);
    }

    public async Task<bool> IsEmpty()
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<bool>("isEmpty", PolylineRef);
    }

    public async Task<Point?> ClosestLayerPoint(Point point)
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        var module = await BundleInterop.GetModule();
        var pointJson = LeafletInterop.ObjectToJson(point);
        var pointObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", pointJson);
        var resJson = await module.InvokeAsync<string>("closestLayerPoint", PolylineRef, pointObject);
        return LeafletInterop.JsonToObject<Point>(resJson);
    }
}