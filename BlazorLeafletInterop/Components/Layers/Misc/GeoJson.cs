using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models.Basics;
using BlazorLeafletInterop.Models.Options.Layer.Misc;
using GeoJSON.Net.Feature;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace BlazorLeafletInterop.Components.Layers.Misc;

public class GeoJson : FeatureGroup
{
    [Parameter] public FeatureCollection Data { get; set; } = new();
    [Parameter] public GeoJsonOptions GeoJsonOptions { get; set; } = new();
    [Parameter] public Func<Feature?, LatLng?, IJSObjectReference>? PointToLayer { get; set; }
    [Parameter] public Func<Feature?, IJSObjectReference>? Style { get; set; }
    [Parameter] public Action<Feature?, IJSObjectReference>? OnEachFeature { get; set; }
    [Parameter] public Func<Feature?, bool>? Filter { get; set; }
    
    [JSInvokable]
    public void OnEachFeatureCallback(string feature, IJSObjectReference layer)
    {
        var featureObject = LeafletInterop.JsonToObject<Feature>(feature);
        OnEachFeature?.Invoke(featureObject, layer);
    }

    [JSInvokable]
    public IJSObjectReference? PointToLayerCallback(string feature, string latLng)
    {
        var featureObject = LeafletInterop.JsonToObject<Feature>(feature);
        var latLngObject = LeafletInterop.JsonToObject<LatLng>(latLng);
        return PointToLayer?.Invoke(featureObject, latLngObject);
    }

    [JSInvokable]
    public IJSObjectReference StyleCallback(string feature)
    {
        var featureObject = LeafletInterop.JsonToObject<Feature>(feature);
        return Style?.Invoke(featureObject);
    }

    [JSInvokable]
    public bool FilterCallback(string feature)
    {
        var featureObject = LeafletInterop.JsonToObject<Feature>(feature);
        return Filter?.Invoke(featureObject) ?? true;
    }

    private IJSObjectReference? GeoJsonRef { get; set; }
    private DotNetObjectReference<GeoJson>? DotNetRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        DotNetRef = DotNetObjectReference.Create(this);
        GeoJsonRef = await CreateGeoJson(Data, GeoJsonOptions.MarkersInheritOptions);
        if (Map is null || GeoJsonRef is null) return;
        await AddTo(Map.MapRef);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) return;
        if (GeoJsonRef is null) return;
        await AddData(Data);
    }

    public async Task<IJSObjectReference> CreateGeoJson(FeatureCollection geoJsonData, bool markersInheritOptions)
    {
        var geoJsonDataJson = LeafletInterop.ObjectToJson(geoJsonData);
        var module = await LayerFactory.GetModule();
        var geoJsonDataObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", geoJsonDataJson);
        return await module.InvokeAsync<IJSObjectReference>("createGeoJson", 
            DotNetObjectReference.Create(this), geoJsonDataObject, markersInheritOptions, 
            PointToLayer is null, Style is null, OnEachFeature is null, Filter is null);
    }

    public async Task<GeoJson> AddTo(IJSObjectReference? map)
    {
        if (GeoJsonRef is null || map is null) throw new NullReferenceException();
        await AddTo<GeoJson>(map, GeoJsonRef);
        return this;
    }
    
    public async Task<GeoJson> AddData(FeatureCollection geoJsonData)
    {
        if (GeoJsonRef is null) throw new NullReferenceException();
        var geoJsonDataJson = LeafletInterop.ObjectToJson(geoJsonData);
        var module = await LayerFactory.GetModule();
        var geoJsonDataObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", geoJsonDataJson);
        await module.InvokeVoidAsync("addData", GeoJsonRef, geoJsonDataObject);
        return this;
    }
}
    
    
    
    