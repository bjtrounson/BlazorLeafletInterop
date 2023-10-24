using BlazorLeafletInterop.Models.Options.Layer.Misc;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Layers.Misc;

public partial class FeatureGroup
{
    public IJSObjectReference? FeatureGroupRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        FeatureGroupRef = await CreateFeatureGroup(LayerGroupOptions);
        if (Map is null || FeatureGroupRef is null) return;
        await AddTo<FeatureGroup>(Map.MapRef, FeatureGroupRef);
    }
    
    public async Task<IJSObjectReference> CreateFeatureGroup(LayerGroupOptions options)
    {
        Module ??= await BundleInterop.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("createFeatureGroup", options);
    }
    
    public async Task<FeatureGroup> SetStyle(object style)
    {
        if (FeatureGroupRef is null) throw new NullReferenceException();
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("setStyle", FeatureGroupRef, style);
        return this;
    }
    
    public async Task<FeatureGroup> BringToFront()
    {
        if (FeatureGroupRef is null) throw new NullReferenceException();
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("bringToFront", FeatureGroupRef);
        return this;
    }
    
    public async Task<FeatureGroup> BringToBack()
    {
        if (FeatureGroupRef is null) throw new NullReferenceException();
        Module ??= await BundleInterop.GetModule();
        await Module.InvokeVoidAsync("bringToBack", FeatureGroupRef);
        return this;
    }
    
    public async Task<IJSObjectReference> GetBounds()
    {
        if (FeatureGroupRef is null) throw new NullReferenceException();
        Module ??= await BundleInterop.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("getBounds", FeatureGroupRef);
    }
}