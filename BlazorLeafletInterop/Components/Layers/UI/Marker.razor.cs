using BlazorLeafletInterop.Components.Layers.Misc;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models.Basics;
using BlazorLeafletInterop.Models.Options.Layer.UI;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Layers.UI;

public partial class Marker : IAsyncDisposable
{
    [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();
    [Parameter] public IJSObjectReference? Icon { get; set; }
    [Parameter] public LatLng LatLng { get; set; } = new();
    [Parameter] public MarkerOptions Options { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    [CascadingParameter(Name = "LayerGroup")] public LayerGroup? LayerGroup { get; set; }
    [CascadingParameter(Name = "MapRef")] public IJSObjectReference? MapRef { get; set; }
    
    public IJSObjectReference? MarkerRef { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        MarkerRef = await CreateMarker(LatLng, Options);
        if (LayerGroup is not null && MarkerRef is not null)
        {
            await LayerGroup.AddLayer(MarkerRef);
            return;
        }
        if (MapRef is null || MarkerRef is null) return;
        await AddTo<Marker>(MapRef, MarkerRef).ConfigureAwait(false);
    }
    
    /// <summary>
    /// Creates a marker with the given options at the given position and adds it to the map.
    /// </summary>
    /// <param name="latLng"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<IJSObjectReference> CreateMarker(LatLng latLng, MarkerOptions options)
    {
        var latLngJson = LeafletInterop.ObjectToJson(latLng);
        var optionsJson = LeafletInterop.ObjectToJson(options);
        var module = await BundleInterop.GetModule();
        var latLngObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", latLngJson);
        var optionsObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", optionsJson);
        var marker = await module.InvokeAsync<IJSObjectReference>("createMarker", latLngObject, optionsObject);
        if (Icon is not null) await SetIcon(marker, Icon);
        return marker;
    }
    
    /// <summary>
    /// Changes the marker position to the given point.
    /// </summary>
    /// <param name="latLng"></param>
    /// <exception cref="NullReferenceException"></exception>
    public async Task SetLatLng(LatLng latLng)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        var latLngJson = LeafletInterop.ObjectToJson(latLng);
        await module.InvokeVoidAsync("setLatLng", MarkerRef, latLngJson);
    }
    
    /// <summary>
    /// Changes the opacity of the marker.
    /// </summary>
    /// <param name="opacity"></param>
    /// <exception cref="NullReferenceException"></exception>
    public async Task SetOpacity(double opacity)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("setOpacity", MarkerRef, opacity);
    }
    
    /// <summary>
    /// Changes the zIndex offset of the marker.
    /// </summary>
    /// <param name="zIndexOffset"></param>
    /// <exception cref="NullReferenceException"></exception>
    public async Task SetZIndexOffset(double zIndexOffset)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("setZIndexOffset", MarkerRef, zIndexOffset);
    }
    
    /// <summary>
    /// Coordinates values are rounded with formatNum function with given precision. Returns a GeoJSON representation of the marker (as a GeoJSON Point Feature).
    /// </summary>
    /// <param name="precision"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<string> ToGeoJson(double? precision)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<string>("toGeoJSON", MarkerRef, precision);
    }
    
    /// <summary>
    /// Gets the popup instance that is bound to this marker.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference?> GetPopup()
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<IJSObjectReference>("getPopup", MarkerRef);
    }
    
    /// <summary>
    /// Opens the popup currently bound to the marker.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference?> OpenPopup()
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<IJSObjectReference>("openPopup", MarkerRef);
    }
    
    /// <summary>
    /// Returns the current geographical position of the marker.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<LatLng?> GetLatLng()
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        var latLngJson = await module.InvokeAsync<string>("getLatLng", MarkerRef);
        return LeafletInterop.JsonToObject<LatLng>(latLngJson);
    }
    
    /// <summary>
    /// Changes the marker icon.
    /// </summary>
    /// <param name="icon"></param>
    /// <exception cref="NullReferenceException"></exception>
    public async Task SetIcon(IJSObjectReference icon)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("setIcon", MarkerRef, icon);
    }

    /// <summary>
    /// Changes the marker icon.
    /// </summary>
    /// <param name="markerRef"></param>
    /// <param name="icon"></param>
    /// <exception cref="NullReferenceException"></exception>
    public async Task SetIcon(IJSObjectReference? markerRef, IJSObjectReference icon)
    {
        if (markerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("setIcon", markerRef, icon);
    }

    public async ValueTask DisposeAsync()
    {
        if (MarkerRef is null) return;
        LayerGroup?.RemoveLayer(MarkerRef);
        if (MapRef is not null) await RemoveFrom<Marker>(MapRef, MarkerRef);
        GC.SuppressFinalize(this);
    }
}