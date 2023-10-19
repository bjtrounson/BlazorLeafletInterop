using System.Runtime.InteropServices.JavaScript;
using BlazorLeafletInterop.Interops;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Base;

public class Evented : ComponentBase
{
    [Inject]
    public IBundleInterop BundleInterop { get; set; } = default!;
    
    public Action<IJSObjectReference>? OnEvent { get; set; } = default!;
    public Action<IJSObjectReference>? OffEvent { get; set; } = default!;
    public Action<IJSObjectReference>? OnceEvent { get; set; } = default!;
    public Action<IJSObjectReference>? AddEventListenerEvent { get; set; } = default!;
    public Action<IJSObjectReference>? RemoveEventListenerEvent { get; set; } = default!;
    public Action<IJSObjectReference>? AddOneTimeEventListenerEvent { get; set; } = default!;
    
    private DotNetObjectReference<Evented>? DotNetRef { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        DotNetRef = DotNetObjectReference.Create(this);
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
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<IJSObjectReference>("on", DotNetRef, layer, eventName);
    }
    
    public async Task<IJSObjectReference> Off(IJSObjectReference layer, string eventName)
    {
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<IJSObjectReference>("off", DotNetRef, layer, eventName);
    }
    
    public async Task<IJSObjectReference> Off(IJSObjectReference layer)
    {
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<IJSObjectReference>("off", DotNetRef, layer);
    }
    
    public async Task<IJSObjectReference> Fire(IJSObjectReference layer, string eventName, IJSObjectReference? data, bool? propagate)
    {
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<IJSObjectReference>("fire", layer, eventName, data, propagate);
    }
    
    public async Task<bool> Listens(IJSObjectReference layer, string eventName, bool? propagate)
    {
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<bool>("listens", DotNetRef, layer, eventName, propagate);
    }
    
    public async Task<IJSObjectReference> Once(IJSObjectReference layer, string eventName)
    {
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<IJSObjectReference>("once", DotNetRef, layer, eventName);
    }
    
    public async Task<IJSObjectReference> AddEventListener(IJSObjectReference layer, string eventName)
    {
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<IJSObjectReference>("addEventListener", DotNetRef, layer, eventName);
    }
    
    public async Task<IJSObjectReference> RemoveEventListener(IJSObjectReference layer, string eventName)
    {
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<IJSObjectReference>("removeEventListener", DotNetRef, layer, eventName);
    }
    
    public async Task<IJSObjectReference> ClearAllEventListeners(IJSObjectReference layer)
    {
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<IJSObjectReference>("clearAllEventListeners", layer);
    }
    
    public async Task<IJSObjectReference> AddOneTimeEventListener(IJSObjectReference layer, string eventName)
    {
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<IJSObjectReference>("addOneTimeEventListener", DotNetRef, layer, eventName);
    }
    
    public async Task<IJSObjectReference> FireEvent(IJSObjectReference layer, string type, JSObject? data)
    {
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<IJSObjectReference>("fireEvent", layer, type, data);
    }
    
    public async Task<bool> HasEventListeners(IJSObjectReference layer, string type)
    {
        var module = await BundleInterop.GetModule();
        return await module.InvokeAsync<bool>("hasEventListeners", layer, type);
    }
}