using BlazorLeafletInterop.Factories;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models.Basics;
using BlazorLeafletInterop.Models.Options.Layer.Vector;
using GeoJSON.Net.Feature;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Layers.Vector;

public class Polyline : Path
{
    [CascadingParameter] public Map? Map { get; set; }
    [Parameter] public PolylineOptions PolylineOptions { get; set; } = new();
    [Parameter] public virtual IEnumerable<LatLng> LatLngs { get; set; } = new List<LatLng>();
    
    public IJSObjectReference? PolylineRef { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if (Map is null) return;
        Module = await LayerFactory.GetModule();
        PolylineRef = await CreatePolyline(PolylineOptions);
        if (PolylineRef is null) return;
        await SetLatLngs(LatLngs);
        await AddTo<Polyline>(Map.MapRef, PolylineRef);
    }
    
    public async Task Initialize(ILayerFactory layerFactory, Map? map, PolylineOptions? options = null)
    {
        if (map is null) return;
        Map = map;
        LayerFactory = layerFactory;
        PolylineRef = await CreatePolyline(options ?? new PolylineOptions());
    }

    public async Task<IJSObjectReference> CreatePolyline(PolylineOptions options)
    {
        Module ??= await LayerFactory.GetModule();
        var optionsJson = LeafletInterop.ObjectToJson(options);
        var optionsObject = await Module.InvokeAsync<IJSObjectReference>("jsonToJsObject", optionsJson);
        return await Module.InvokeAsync<IJSObjectReference>("createPolyline", LatLngs, optionsObject);
    }
    
    public async Task<Polyline> SetLatLngs(IEnumerable<LatLng> latLngs)
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeAsync<IJSObjectReference>("setLatLngs", PolylineRef, latLngs);
        return this;
    }

    public async Task<Polyline> AddLatLng(LatLng latLng)
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeAsync<IJSObjectReference>("addLatLng", PolylineRef, latLng);
        return this;
    }
    
    public async Task<LatLng[]?> GetLatLngs()
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        Module ??= await LayerFactory.GetModule();
        var latLngsJson = await Module.InvokeAsync<string>("getLatLngs", PolylineRef);
        return LeafletInterop.JsonToObject<LatLng[]>(latLngsJson);
    }
    
    public async Task<FeatureCollection?> ToGeoJson()
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        Module ??= await LayerFactory.GetModule();
        var geojson = await Module.InvokeAsync<string>("toGeoJSON", PolylineRef);
        return LeafletInterop.JsonToObject<FeatureCollection>(geojson);
    }
    
    public async Task<LatLngBounds?> GetBounds()
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        Module ??= await LayerFactory.GetModule();
        var boundsJson = await Module.InvokeAsync<string>("getBounds", PolylineRef);
        return LeafletInterop.JsonToObject<LatLngBounds>(boundsJson);
    }

    public async Task<LatLng?> GetCenter()
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        Module ??= await LayerFactory.GetModule();
        var latLngJson = await Module.InvokeAsync<string>("getCenter", PolylineRef);
        return LeafletInterop.JsonToObject<LatLng>(latLngJson);
    }

    public async Task<bool> IsEmpty()
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<bool>("isEmpty", PolylineRef);
    }

    public async Task<Point?> ClosestLayerPoint(Point point)
    {
        if (PolylineRef is null) throw new NullReferenceException("Ref or map is null");
        Module ??= await LayerFactory.GetModule();
        var resJson = await Module.InvokeAsync<string>("closestLayerPoint", PolylineRef, point);
        return LeafletInterop.JsonToObject<Point>(resJson);
    }
}