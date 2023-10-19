using BlazorLeafletInterop.Interops;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components;

public partial class FeatureGroup
{
    public IJSObjectReference? FeatureGroupRef { get; set; }
    
    public async Task<IJSObjectReference> CreateFeatureGroup(LayerGroup options)
    {
        var module = await BundleInterop.GetModule();
        var optionsJson = LeafletInterop.ObjectToJson(options);
        var optionsObject = await module.InvokeAsync<IJSObjectReference>("jsonToJsObject", optionsJson);
        return await module.InvokeAsync<IJSObjectReference>("createFeatureGroup", optionsObject);
    }
    
    public async Task<FeatureGroup> SetStyle(object style)
    {
        if (FeatureGroupRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("setStyle", FeatureGroupRef, style);
        return this;
    }
    
    public async Task<FeatureGroup> BringToFront()
    {
        if (FeatureGroupRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("bringToFront", FeatureGroupRef);
        return this;
    }
    
    public async Task<FeatureGroup> BringToBack()
    {
        if (FeatureGroupRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        await module.InvokeVoidAsync("bringToBack", FeatureGroupRef);
        return this;
    }
    
    public async Task<IJSObjectReference> GetBounds()
    {
        if (FeatureGroupRef is null) throw new NullReferenceException();
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<IJSObjectReference>("getBounds", FeatureGroupRef);
    }
}