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
    [CascadingParameter(Name = "Map")] public Map? Map { get; set; }
    
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
        if (Map is null || MarkerRef is null) return;
        await AddTo<Marker>(Map.MapRef, MarkerRef).ConfigureAwait(false);
    }

    public async Task Initialize(IBundleInterop interop, Map map, LatLng? latLng, MarkerOptions? options)
    {
        BundleInterop = interop;
        Map = map;
        LatLng = latLng ?? new LatLng();
        Options = options ?? new MarkerOptions();
        MarkerRef = await CreateMarker(LatLng, Options);
        if (LayerGroup is not null && MarkerRef is not null)
        {
            await LayerGroup.AddLayer(MarkerRef);
            return;
        }
        await AddTo<Marker>(Map.MapRef, MarkerRef).ConfigureAwait(false);
    }
    
    /// <summary>
    /// Creates a marker with the given options at the given position and adds it to the map.
    /// </summary>
    /// <param name="latLng"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<IJSObjectReference> CreateMarker(LatLng latLng, MarkerOptions options)
    {
        Module ??= await BundleInterop.GetModule();
        var optionsJson = LeafletInterop.ObjectToJson(options);
        var optionsObject = await Module.InvokeAsync<IJSObjectReference>("jsonToJsObject", optionsJson);
        var marker = await Module.InvokeAsync<IJSObjectReference>("createMarker", latLng, optionsObject);
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
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("setLatLng", MarkerRef, latLng);
    }
    
    /// <summary>
    /// Changes the opacity of the marker.
    /// </summary>
    /// <param name="opacity"></param>
    /// <exception cref="NullReferenceException"></exception>
    public async Task SetOpacity(double opacity)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("setOpacity", MarkerRef, opacity);
    }
    
    /// <summary>
    /// Changes the zIndex offset of the marker.
    /// </summary>
    /// <param name="zIndexOffset"></param>
    /// <exception cref="NullReferenceException"></exception>
    public async Task SetZIndexOffset(double zIndexOffset)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("setZIndexOffset", MarkerRef, zIndexOffset);
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
        Module ??= await BundleInterop.GetModule();
        return await Module.InvokeAsync<string>("toGeoJSON", MarkerRef, precision);
    }
    
    /// <summary>
    /// Gets the popup instance that is bound to this marker.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference?> GetPopup()
    {
        if (MarkerRef is null) throw new NullReferenceException();
        Module ??= await BundleInterop.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("getPopup", MarkerRef);
    }
    
    /// <summary>
    /// Opens the popup currently bound to the marker.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference?> OpenPopup()
    {
        if (MarkerRef is null) throw new NullReferenceException();
        Module ??= await BundleInterop.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("openPopup", MarkerRef);
    }
    
    /// <summary>
    /// Returns the current geographical position of the marker.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<LatLng?> GetLatLng()
    {
        if (MarkerRef is null) throw new NullReferenceException();
        Module ??= await BundleInterop.GetModule();
        var latLngJson = await Module.InvokeAsync<string>("getLatLng", MarkerRef);
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
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("setIcon", MarkerRef, icon);
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
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("setIcon", markerRef, icon);
    }

    public async ValueTask DisposeAsync()
    {
        if (MarkerRef is null) return;
        LayerGroup?.RemoveLayer(MarkerRef);
        if (Map is not null) await RemoveFrom<Marker>(Map.MapRef, MarkerRef);
        GC.SuppressFinalize(this);
    }
}