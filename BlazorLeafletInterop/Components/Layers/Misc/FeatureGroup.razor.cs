using BlazorLeafletInterop.Models.Options.Layer.Vector;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Layers.Misc;

public partial class FeatureGroup
{
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        JsObjectReference = await LayerFactory.CreateFeatureGroup(LayerGroupOptions);
        if (Map is null || JsObjectReference is null) return;
        await AddTo<FeatureGroup>(Map.MapRef, JsObjectReference);
    }
    
    /// <summary>
    /// Brings the layer group to the top of all other layers.
    /// </summary>
    /// <returns>FeatureGroup</returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<FeatureGroup> BringToFront()
    {
        if (JsObjectReference is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("bringToFront", JsObjectReference);
        return this;
    }
    
    /// <summary>
    /// Brings the layer group to the back of all other layers
    /// </summary>
    /// <returns>FeatureGroup</returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<FeatureGroup> BringToBack()
    {
        if (JsObjectReference is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("bringToBack", JsObjectReference);
        return this;
    }
    
    /// <summary>
    /// Returns the LatLngBounds of the Feature Group (created from bounds and coordinates of its children).
    /// </summary>
    /// <returns>IJSObjectReference</returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> GetBounds()
    {
        if (JsObjectReference is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("getBounds", JsObjectReference);
    }

    /// <summary>
    /// Sets the given path options to each layer of the group that has a setStyle method.
    /// </summary>
    /// <param name="options"></param>
    /// <returns>FeatureGroup</returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<FeatureGroup> SetStyle(PathOptions options)
    {
        if (JsObjectReference is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("setStyle", JsObjectReference, options);
        return this;
    }
}