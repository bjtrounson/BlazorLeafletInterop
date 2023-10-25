using BlazorLeafletInterop.Components.Base;
using BlazorLeafletInterop.Models.Options.Control;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Controls;

public class ControlScale : Control
{
    [Parameter] public ControlScaleOptions ScaleOptions { get; set; } = new();
    
    private IJSObjectReference? ScaleRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        ScaleRef = await CreateLayers(ScaleOptions);
        if (Map is null || ScaleRef is null) return;
        await AddTo<ControlScale>(Map.MapRef, ScaleRef);
    }
    
    private async Task<IJSObjectReference> CreateLayers(ControlScaleOptions? scaleOptions)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("createScale", scaleOptions);
    }
}