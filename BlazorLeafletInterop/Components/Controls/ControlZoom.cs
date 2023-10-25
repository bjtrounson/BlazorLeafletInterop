using BlazorLeafletInterop.Components.Base;
using BlazorLeafletInterop.Models.Options.Control;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Controls;

public class ControlZoom : Control
{
    [Parameter] public ControlZoomOptions ZoomOptions { get; set; } = new();
    
    private IJSObjectReference? ZoomRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        ZoomRef = await CreateZoom(ZoomOptions);
        if (MapRef is null || ZoomRef is null) return;
        await AddTo<ControlZoom>(MapRef, ZoomRef);
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        if (ZoomRef is null) return;
        // Remove the old zoom control
        await Remove<ControlZoom>(ZoomRef);
        // Create a new zoom control
        ZoomRef = await CreateZoom(ZoomOptions);
        if (MapRef is null || ZoomRef is null) return;
        await AddTo<ControlZoom>(MapRef, ZoomRef);
    }

    private async Task<IJSObjectReference> CreateZoom(ControlZoomOptions? zoomOptions)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("createZoom", zoomOptions);
    }
}