using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models;
using BlazorLeafletInterop.Models.Basics;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components;

[SupportedOSPlatform("browser")]
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
        if (!OperatingSystem.IsBrowser()) throw new PlatformNotSupportedException();
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
    
    public async Task SetLatLng(LatLng latLng)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        var latLngJson = LeafletInterop.ObjectToJson(latLng);
        await module.InvokeVoidAsync("setLatLng", MarkerRef, latLngJson);
    }
    
    public async Task SetOpacity(double opacity)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("setOpacity", MarkerRef, opacity);
    }
    
    public async Task SetZIndexOffset(double zIndexOffset)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("setZIndexOffset", MarkerRef, zIndexOffset);
    }
    
    public async Task<string> ToGeoJson(double? precision)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<string>("toGeoJSON", MarkerRef, precision);
    }
    
    public async Task<IJSObjectReference?> GetPopup()
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<IJSObjectReference>("getPopup", MarkerRef);
    }
    
    public async Task<IJSObjectReference?> OpenPopup()
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<IJSObjectReference>("openPopup", MarkerRef);
    }
    
    public async Task<LatLng?> GetLatLng()
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        var latLngJson = await module.InvokeAsync<string>("getLatLng", MarkerRef);
        return LeafletInterop.JsonToObject<LatLng>(latLngJson);
    }
    
    public async Task SetIcon(IJSObjectReference icon)
    {
        if (MarkerRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("setIcon", MarkerRef, icon);
    }

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