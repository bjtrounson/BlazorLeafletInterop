using BlazorLeafletInterop.Models;
using BlazorLeafletInterop.Models.Options.Layer.UI;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Components.Layers.UI;

public partial class Tooltip : IAsyncDisposable
{
    [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();
    [Parameter] public TooltipOptions TooltipOptions { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [CascadingParameter( Name = "MarkerRef")] public IJSObjectReference? MarkerRef { get; set; }
    
    public IJSObjectReference? TooltipRef { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (MarkerRef is null) return;
        Module ??= await LayerFactory.GetModule();
        var tooltipContent = await Module.InvokeAsync<string>("getElementInnerHtml", Id);
        await BindTooltip(MarkerRef, tooltipContent, TooltipOptions);
        TooltipRef = await GetTooltip(MarkerRef);
    }

    public async ValueTask DisposeAsync()
    {
        if (MarkerRef is null) return;
        await UnbindTooltip(MarkerRef);
        if (TooltipRef != null) await TooltipRef.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}