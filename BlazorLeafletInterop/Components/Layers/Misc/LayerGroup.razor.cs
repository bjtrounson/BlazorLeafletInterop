using BlazorLeafletInterop.Models.Options.Layer.Misc;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Layers.Misc;

public partial class LayerGroup : IAsyncDisposable
{
    [Parameter] public LayerGroupOptions LayerGroupOptions { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    [CascadingParameter(Name = "Map")] public Map? Map { get; set; }
    
    private Action<IJSObjectReference>? EachLayerCallback { get; set; }
    
    public IJSObjectReference? JsObjectReference { get; set; }
    
    [JSInvokable]
    public void EachLayerCallbackInvoke(IJSObjectReference layer) => EachLayerCallback?.Invoke(layer);
    
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        JsObjectReference = await LayerFactory.CreateLayerGroup(LayerGroupOptions);
        if (Map is null || JsObjectReference is null) return;
        await AddTo<LayerGroup>(Map.MapRef, JsObjectReference);
    }
    
    /// <summary>
    /// Adds the given layer to the group.
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<LayerGroup> AddLayer(IJSObjectReference layer)
    {
        if (JsObjectReference is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("addLayer", JsObjectReference, layer);
        return this;
    }

    /// <summary>
    /// Removes the given layer from the group.
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<LayerGroup> RemoveLayer(IJSObjectReference layer)
    {
        if (JsObjectReference is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("removeLayer", JsObjectReference, layer);
        return this;
    }

    /// <summary>
    /// Returns true if the given layer is currently added to the group.
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<bool> HasLayer(IJSObjectReference layer)
    {
        if (JsObjectReference is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<bool>("hasLayer", JsObjectReference, layer);
    }

    /// <summary>
    /// Removes all the layers from the group.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<LayerGroup> ClearLayers()
    {
        if (JsObjectReference is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("clearLayers", JsObjectReference);
        return this;
    }
    
    /// <summary>
    /// Calls methodName on every layer contained in this group, passing any additional parameters.
    /// Has no effect if the layers contained do not implement methodName.
    /// </summary>
    /// <param name="methodName"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<LayerGroup> Invoke(string methodName, params object[] args)
    {
        if (JsObjectReference is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("invoke", JsObjectReference, methodName, args);
        return this;
    }

    /// <summary>
    /// Iterates over the layers of the group executes the callback for each layer.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<LayerGroup> EachLayer(Action<IJSObjectReference> action)
    {
        EachLayerCallback = action;
        if (JsObjectReference is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("eachLayer", DotNetObjectReference.Create(this), "EachLayerCallbackInvoke", JsObjectReference);
        return this;
    }

    /// <summary>
    /// Returns the layer with the given internal ID.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> GetLayer(int index)
    {
        if (JsObjectReference is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("getLayer", JsObjectReference, index);
    }
    
    /// <summary>
    /// Returns the internal ID for a layer
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<int> GetLayerId(IJSObjectReference layer)
    {
        if (JsObjectReference is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<int>("getLayerId", JsObjectReference, layer);
    }
    
    /// <summary>
    /// Returns an array of all the layers added to the group.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference[]> GetLayers()
    {
        if (JsObjectReference is null) throw new NullReferenceException();
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference[]>("getLayers", JsObjectReference);
    }

    public virtual async ValueTask DisposeAsync()
    {
        if (JsObjectReference is null || Map is null) return;
        await RemoveFrom<LayerGroup>(Map.MapRef, JsObjectReference);
        if (JsObjectReference != null) await JsObjectReference.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}