namespace BlazorLeafletInterop.Models;

public class InteractiveLayerOptions : LayerOptions
{
    public virtual bool Interactive { get; set; } = true;
    public virtual bool BubblingMouseEvents { get; set; } = true;
}