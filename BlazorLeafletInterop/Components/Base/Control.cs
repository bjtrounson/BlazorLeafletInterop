using BlazorLeafletInterop.Factories;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Base;

public class Control : ComponentBase
{
    [Inject]
    public ILayerFactory LayerFactory { get; set; } = default!;
    
    [CascadingParameter(Name = "Map")] public Map? Map { get; set; }

    protected IJSObjectReference? Module { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await base.OnAfterRenderAsync(firstRender);
        Module ??= await LayerFactory.GetModule();
    }
    
    /// <summary>
    /// Adds the control to the map.
    /// </summary>
    /// <param name="map"></param>
    /// <param name="ref"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>this</returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<T> AddTo<T>(IJSObjectReference? map, IJSObjectReference? @ref)
    {
        if (@ref is null || map is null) throw new NullReferenceException("Ref or map is null");
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("addTo", @ref, map);
        return (T)(object)this;
    }

    /// <summary>
    /// Removes the control from the map.
    /// </summary>
    /// <param name="ref"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>this</returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<T> Remove<T>(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("remove", @ref);
        return (T)(object)this;
    }

    /// <summary>
    /// Returns the current position of the control.
    /// </summary>
    /// <param name="ref"></param>
    /// <returns>string</returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<string> GetPosition(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<string>("getPosition", @ref);
    }

    /// <summary>
    /// Sets the current position of the control.
    /// </summary>
    /// <param name="ref"></param>
    /// <param name="position"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>this</returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<T> SetPosition<T>(IJSObjectReference? @ref, string position)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await LayerFactory.GetModule();
        await Module.InvokeVoidAsync("setPosition", @ref, position);
        return (T)(object)this;
    }

    /// <summary>
    /// Returns the HTMLElement representing the control.
    /// </summary>
    /// <param name="ref"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>The IJSObjectReference for the HTMLElement</returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IJSObjectReference> GetContainer<T>(IJSObjectReference? @ref)
    {
        if (@ref is null) throw new NullReferenceException("Ref is null");
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("getContainer", @ref);
    }
}