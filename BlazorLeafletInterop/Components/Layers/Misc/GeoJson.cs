using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models.Options.Layer.Misc;
using GeoJSON.Net.Feature;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Layers.Misc;

public class GeoJson : FeatureGroup
{
    [Parameter] public FeatureCollection Data { get; set; } = new();
    [Parameter] public GeoJsonOptions GeoJsonOptions { get; set; } = new();
    [Parameter] public Func<IJSObjectReference, IJSObjectReference, IJSObjectReference>? PointToLayer { get; set; }
    [Parameter] public Action<IJSObjectReference>? Style { get; set; }
    [Parameter] public Action<IJSObjectReference, IJSObjectReference>? OnEachFeature { get; set; }
    [Parameter] public Action<IJSObjectReference>? Filter { get; set; }
    
    [JSInvokable]
    public void OnEachFeatureCallback(IJSObjectReference feature, IJSObjectReference layer) => OnEachFeature?.Invoke(feature, layer);
    
    [JSInvokable]
    public IJSObjectReference? PointToLayerCallback(IJSObjectReference feature, IJSObjectReference latLng) => PointToLayer?.Invoke(feature, latLng);
    
    [JSInvokable]
    public void StyleCallback(IJSObjectReference feature) => Style?.Invoke(feature);
    
    [JSInvokable]
    public void FilterCallback(IJSObjectReference feature) => Filter?.Invoke(feature);

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
        var module = await BundleInterop.GetModule();
        var geoJsonDataObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", geoJsonDataJson);
        return await module.InvokeAsync<IJSObjectReference>("createGeoJson", 
            DotNetRef, geoJsonDataObject, markersInheritOptions, 
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
        var module = await BundleInterop.GetModule();
        var geoJsonDataObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", geoJsonDataJson);
        await module.InvokeVoidAsync("addData", GeoJsonRef, geoJsonDataObject);
        return this;
    }
}
    
    
    
    