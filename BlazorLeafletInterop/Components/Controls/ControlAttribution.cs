using BlazorLeafletInterop.Components.Base;
using BlazorLeafletInterop.Models.Options.Control;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Controls;

public class ControlAttribution : Control
{
    [Parameter] public ControlAttributionOptions AttributionOptions { get; set; } = new();
    
    private IJSObjectReference? AttributionRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        AttributionRef = await CreateAttribution(AttributionOptions);
        if (MapRef is null || AttributionRef is null) return;
        await AddTo<ControlAttribution>(MapRef, AttributionRef);
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        if (AttributionRef is null) return;
        // Remove the old attribution control
        await Remove<ControlAttribution>(AttributionRef);
        // Create a new attribution control
        AttributionRef = await CreateAttribution(AttributionOptions);
        if (MapRef is null || AttributionRef is null) return;
        await AddTo<ControlAttribution>(MapRef, AttributionRef);
    }

    private async Task<IJSObjectReference> CreateAttribution(ControlAttributionOptions? attributionOptions)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("createAttribution", attributionOptions);
    }
    
    public async Task<ControlAttribution> AddAttribution(string attribution)
    {
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("addAttribution", AttributionRef, attribution);
        return this;
    }
    
    public async Task<ControlAttribution> RemoveAttribution(string attribution)
    {
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("removeAttribution", AttributionRef, attribution);
        return this;
    }
    
    public async Task<ControlAttribution> SetPrefix(string prefix)
    {
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("setPrefix", AttributionRef, prefix);
        return this;
    }
}