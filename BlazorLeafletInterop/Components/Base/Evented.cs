﻿using System.Runtime.InteropServices.JavaScript;
using BlazorLeafletInterop.Factories;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Base;

public class Evented : ComponentBase
{
    [Inject]
    public ILayerFactory LayerFactory { get; set; } = default!;
    
    public Action<IJSObjectReference>? OnEvent { get; set; } = default!;
    public Action<IJSObjectReference>? OffEvent { get; set; } = default!;
    public Action<IJSObjectReference>? OnceEvent { get; set; } = default!;
    public Action<IJSObjectReference>? AddEventListenerEvent { get; set; } = default!;
    public Action<IJSObjectReference>? RemoveEventListenerEvent { get; set; } = default!;
    public Action<IJSObjectReference>? AddOneTimeEventListenerEvent { get; set; } = default!;
    
    private DotNetObjectReference<Evented>? DotNetRef { get; set; }
    public IJSObjectReference? Module { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await base.OnInitializedAsync();
        DotNetRef = DotNetObjectReference.Create(this);
        Module = await LayerFactory.GetModule();
    }
    
    [JSInvokable]
    public void OnEventCallback(IJSObjectReference eventObject) => OnEvent?.Invoke(eventObject);
    
    [JSInvokable]
    public void OffEventCallback(IJSObjectReference eventObject) => OffEvent?.Invoke(eventObject);
    
    [JSInvokable]
    public void OnceEventCallback(IJSObjectReference eventObject) => OnceEvent?.Invoke(eventObject);
    
    [JSInvokable]
    public void AddEventListenerEventCallback(IJSObjectReference eventObject) => AddEventListenerEvent?.Invoke(eventObject);
    
    [JSInvokable]
    public void RemoveEventListenerEventCallback(IJSObjectReference eventObject) => RemoveEventListenerEvent?.Invoke(eventObject);
    
    [JSInvokable]
    public void AddOneTimeEventListenerEventCallback(IJSObjectReference eventObject) => AddOneTimeEventListenerEvent?.Invoke(eventObject);
    
    public async Task<IJSObjectReference> On(IJSObjectReference layer, string eventName)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("on", DotNetRef, layer, eventName);
    }
    
    public async Task<IJSObjectReference> Off(IJSObjectReference layer, string eventName)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("off", DotNetRef, layer, eventName);
    }
    
    public async Task<IJSObjectReference> Off(IJSObjectReference layer)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("off", DotNetRef, layer);
    }
    
    public async Task<IJSObjectReference> Fire(IJSObjectReference layer, string eventName, IJSObjectReference? data, bool? propagate)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("fire", layer, eventName, data, propagate);
    }
    
    public async Task<bool> Listens(IJSObjectReference layer, string eventName, bool? propagate)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<bool>("listens", DotNetRef, layer, eventName, propagate);
    }
    
    public async Task<IJSObjectReference> Once(IJSObjectReference layer, string eventName)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("once", DotNetRef, layer, eventName);
    }
    
    public async Task<IJSObjectReference> AddEventListener(IJSObjectReference layer, string eventName)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("addEventListener", DotNetRef, layer, eventName);
    }
    
    public async Task<IJSObjectReference> RemoveEventListener(IJSObjectReference layer, string eventName)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("removeEventListener", DotNetRef, layer, eventName);
    }
    
    public async Task<IJSObjectReference> ClearAllEventListeners(IJSObjectReference layer)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("clearAllEventListeners", layer);
    }
    
    public async Task<IJSObjectReference> AddOneTimeEventListener(IJSObjectReference layer, string eventName)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("addOneTimeEventListener", DotNetRef, layer, eventName);
    }
    
    public async Task<IJSObjectReference> FireEvent(IJSObjectReference layer, string type, JSObject? data)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<IJSObjectReference>("fireEvent", layer, type, data);
    }
    
    public async Task<bool> HasEventListeners(IJSObjectReference layer, string type)
    {
        Module ??= await LayerFactory.GetModule();
        return await Module.InvokeAsync<bool>("hasEventListeners", layer, type);
    }
}