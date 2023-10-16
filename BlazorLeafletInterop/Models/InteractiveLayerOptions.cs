namespace BlazorLeafletInterop.Models;

public class InteractiveLayerOptions : LayerOptions
{
    public bool Interactive { get; set; } = true;
    public virtual bool BubblingMouseEvents { get; set; } = true;
}