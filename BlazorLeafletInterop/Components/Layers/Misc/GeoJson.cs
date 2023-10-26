using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models.Basics;
using BlazorLeafletInterop.Models.Options.Layer.Misc;
using BlazorLeafletInterop.Models.Options.Layer.Vector;
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
    [Parameter] public Func<Feature?, PathOptions>? Style { get; set; }
    [Parameter] public Action<Feature?, IJSObjectReference>? OnEachFeature { get; set; }
    [Parameter] public Func<Feature?, bool>? Filter { get; set; }
    
    private Action<IJSObjectReference, Feature?>? EachLayerCallback { get; set; }
    
    [JSInvokable]
    public void OnEachFeatureCallback(string feature, IJSObjectReference layer)
    {
        var featureObject = LeafletInterop.JsonToObject<Feature>(feature);
        OnEachFeature?.Invoke(featureObject, layer);
    }
    
    
    [JSInvokable]
    public void OnEachFeatureCallback(IJSObjectReference layer, string feature)
    {
        var featureObject = LeafletInterop.JsonToObject<Feature>(feature);
        EachLayerCallback?.Invoke(layer, featureObject);
    }

    [JSInvokable]
    public IJSObjectReference? PointToLayerCallback(string feature, string latLng)
    {
        var featureObject = LeafletInterop.JsonToObject<Feature>(feature);
        var latLngObject = LeafletInterop.JsonToObject<LatLng>(latLng);
        return PointToLayer?.Invoke(featureObject, latLngObject);
    }

    [JSInvokable]
    public PathOptions? StyleCallback(string feature)
    {
        var featureObject = LeafletInterop.JsonToObject<Feature>(feature);
        return Style is null ? new PathOptions() : Style?.Invoke(featureObject);
    }

    [JSInvokable]
    public bool FilterCallback(string feature)
    {
        var featureObject = LeafletInterop.JsonToObject<Feature>(feature);
        return Filter?.Invoke(featureObject) ?? true;
    }
    
    private DotNetObjectReference<GeoJson>? DotNetRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        DotNetRef = DotNetObjectReference.Create(this);
        JsObjectReference = await CreateGeoJson(Data, GeoJsonOptions.MarkersInheritOptions);
        if (Map is null || JsObjectReference is null) return;
        await AddTo<GeoJson>(Map.MapRef, JsObjectReference);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        if (JsObjectReference is null) return;
        await AddData(Data);
    }

    protected override async Task OnParametersSetAsync()
    {
        if (JsObjectReference is null) return;
        await ClearLayers();
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
    
    public async Task<GeoJson> AddData(FeatureCollection geoJsonData)
    {
        if (JsObjectReference is null) throw new NullReferenceException();
        var geoJsonDataJson = LeafletInterop.ObjectToJson(geoJsonData);
        var module = await LayerFactory.GetModule();
        var geoJsonDataObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", geoJsonDataJson);
        await module.InvokeVoidAsync("addData", JsObjectReference, geoJsonDataObject);
        return this;
    }
    
    public async Task<GeoJson> ResetStyle(IJSObjectReference? layer = null)
    {
        if (JsObjectReference is null) throw new NullReferenceException();
        var module = await LayerFactory.GetModule();
        await module.InvokeVoidAsync("resetStyle", JsObjectReference, layer);
        return this;
    }
    
    public async Task EachLayer(Action<IJSObjectReference, Feature?> callback)
    {
        EachLayerCallback = callback;
        if (JsObjectReference is null) throw new NullReferenceException();
        var module = await LayerFactory.GetModule();
        await module.InvokeVoidAsync("eachLayerGeoJson",  DotNetObjectReference.Create(callback), "EachLayerCallback", JsObjectReference);
    }
}
    
    
    
    